using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Shared.Families
{
    [IgnoreAntiforgeryToken]

    [Authorize]
    public class IndexMyMemberShipsModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public IndexMyMemberShipsModel(AppDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public PaginatedList<Family_User> FamilyLeave { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";
        public string SearchQuery { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            const int pageSize = 10;

            var user = await _userManager.GetUserAsync(User);

            var query = _context.Family_Users
                .Where(fu => fu.UserId == user.Id)
                .Include(fu => fu.Family)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearchString = searchString.ToLower();
                query = query.Where(a =>
                    a.Family.Name.ToLower().Contains(normalizedSearchString) ||
                    a.Family.Description.ToLower().Contains(normalizedSearchString));
            }

            query = SortColumn switch
            {
                "Name" => SortOrder == "asc" ? query.OrderBy(a => a.Family.Name) : query.OrderByDescending(a => a.Family.Name),
                "Description" => SortOrder == "asc" ? query.OrderBy(a => a.Family.Description) : query.OrderByDescending(a => a.Family.Description),
                _ => query.OrderBy(a => a.Family.Name),
            };

            FamilyLeave = await PaginatedList<Family_User>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);
        }
        public async Task<IActionResult> OnGetMembersAsync(string familyId, string currentUserId)
        {
            var members = await _context.Family_Users
                .Where(fu => fu.FamilyId == familyId && fu.UserId != currentUserId && fu.Is_manager == false)
                .Include(fu => fu.User) 
                .ToListAsync();

            var memberDTOs = members.Select(m => new
            {
                m.UserId,
                m.User.UserName 
            }).ToList();

            return new JsonResult(memberDTOs);
        }
        public async Task<IActionResult> OnPostUpdateManagerAndQuitAsync(string familyId, string newManagerId, string currentUserId)
        {
                if (string.IsNullOrEmpty(familyId) || string.IsNullOrEmpty(newManagerId) || string.IsNullOrEmpty(currentUserId))
                {
                    return BadRequest("Des informations sont manquantes.");
                }

                var familyUsers = await _context.Family_Users
                    .Where(fu => fu.FamilyId == familyId)
                    .ToListAsync();

                if (familyUsers == null || familyUsers.Count == 0)
                {
                    return NotFound("Famille introuvable ou aucun membre dans cette famille.");
                }

                var newManager = familyUsers.FirstOrDefault(fu => fu.UserId == newManagerId);
                if (newManager != null)
                {
                    newManager.Is_manager = true;
                }
                else
                {
                    return NotFound("Le nouveau manager sélectionné est introuvable dans cette famille.");
                }

                var userNewManager = await _userManager.FindByIdAsync(newManagerId);
                var familyName = await _context.Families
                    .Where(f => f.Id == familyId)
                    .Select(f => f.Name) 
                    .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(familyName))
                {
                    throw new Exception("La famille spécifiée n'a pas été trouvée.");
                }

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(userNewManager);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
           
                await _emailSender.SendEmailAsync(
                 userNewManager.Email, 
                 "Vous êtes maintenant manager de l'équipe", 
                 $"Bonjour {userNewManager.UserName},<br><br>" +
                 $"Félicitations ! Vous êtes désormais le manager de l'équipe <strong>{familyName}</strong>.<br>" +
                 $"Nous vous remercions pour votre implication.<br><br>" +
                 $"Cordialement,<br>L'équipe."
                 );

                var user = await _userManager.FindByNameAsync(currentUserId);
                var userId = user?.Id; 

                var currentUser = familyUsers.FirstOrDefault(fu => fu.UserId == userId);
                if (currentUser != null)
                {
                    _context.Family_Users.Remove(currentUser);
                }
                else
                {
                    return NotFound("L'utilisateur actuel est introuvable dans cette famille.");
                }

                var userMemberShip = _context.MemberShip_Families.Where(fu => fu.RequesterId == userId);
                _context.MemberShip_Families.RemoveRange(userMemberShip);

                await _context.SaveChangesAsync();
         
                return new JsonResult(new { message = "Le nouveau manager a été défini et vous avez quitté la famille avec succès." });
        }


        public async Task<IActionResult> OnPostQuitFamilyAsync(string familyId)
        {
            var user = await _userManager.GetUserAsync(User);
            var familyUser = await _context.Family_Users
                .FirstOrDefaultAsync(fu => fu.UserId == user.Id && fu.FamilyId == familyId);

            if (familyUser != null)
            {
                _context.Family_Users.Remove(familyUser);

                var userMemberShip = _context.MemberShip_Families.Where(fu => fu.RequesterId == user.Id);
                _context.MemberShip_Families.RemoveRange(userMemberShip);

                await _context.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }

            return BadRequest("Erreur lors de la suppression de l'association.");
        }
        public async Task<IActionResult> OnPostDeleteFamilyAndAssociationAsync(string familyId)
        {
            if (string.IsNullOrEmpty(familyId))
            {
                return BadRequest("ID de famille invalide.");
            }

            var family = await _context.Families
                .FirstOrDefaultAsync(f => f.Id == familyId);

            if (family == null)
            {
                return NotFound("Famille non trouvée.");
            }

            var user = await _userManager.GetUserAsync(User);
            var familyUser = await _context.Family_Users
                .FirstOrDefaultAsync(fu => fu.UserId == user.Id && fu.FamilyId == familyId);

            if (familyUser != null)
            {
                _context.Family_Users.Remove(familyUser);
                await _context.SaveChangesAsync();
            }
               
                _context.Families.Remove(family);
            await _context.SaveChangesAsync();

            var userMemberShip = _context.MemberShip_Families.Where(fu => fu.RequesterId == user.Id);
            _context.MemberShip_Families.RemoveRange(userMemberShip);

            return RedirectToPage(); 
        }    
    }
}
