using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.DTO.Admin.Users;
using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestionnaire_Collections.Pages.Admin.Users
{
    [IgnoreAntiforgeryToken]

    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public IndexModel(AppDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        public PaginatedList<UserViewModelIndex> Users { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";
        public string SearchQuery { get; set; }

        List<string> familyEmpty = new List<string>();

        public async Task OnGetAsync(string searchString, string emailConfirmedFilter, string isAdminFilter, string isActiveFilter)
        {
            const int pageSize = 10;

            var query = _userManager.Users
                .Select(u => new UserViewModelIndex
                {
                    Login = u.UserName,
                    Email = u.Email,
                    EmailConfirmed = u.EmailConfirmed,
                    IsAdmin = u.IsAdmin,
                    IsActive = u.IsActive
                });

            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearchString = searchString.ToLower();
                query = query.Where(a =>
                    a.Login.ToLower().Contains(normalizedSearchString) ||
                    a.Email.ToLower().Contains(normalizedSearchString));
            }

            query = SortColumn switch
            {
                "Login" => SortOrder == "asc" ? query.OrderBy(a => a.Login) : query.OrderByDescending(a => a.Login),
                "Email" => SortOrder == "asc" ? query.OrderBy(a => a.Email) : query.OrderByDescending(a => a.Email),
                _ => query.OrderBy(a => a.Login),
            };

            if (!string.IsNullOrEmpty(emailConfirmedFilter))
            {
                bool emailConfirmed;
                if (bool.TryParse(emailConfirmedFilter, out emailConfirmed))
                {
                    query = query.Where(u => u.EmailConfirmed == emailConfirmed);
                }
            }

            if (!string.IsNullOrEmpty(isAdminFilter))
            {
                bool isAdmin;
                if (bool.TryParse(isAdminFilter, out isAdmin))
                {
                    query = query.Where(u => u.IsAdmin == isAdmin);
                }
            }

            if (!string.IsNullOrEmpty(isActiveFilter))
            {
                bool isActive;
                if (bool.TryParse(isActiveFilter, out isActive))
                {
                    query = query.Where(u => u.IsActive == isActive);
                }
            }

            Users = await PaginatedList<UserViewModelIndex>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);
        }

        public async Task<IActionResult> OnGetManagerStatusAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null)
            {
                return NotFound();
            }

            List<string> familyNotEmpty = await GetFamiliesManager(user);

            return new JsonResult(new
            {
                canDelete = familyNotEmpty.Count == 0
            });
        }

        private async Task<List<string>> GetFamiliesManager(ApplicationUser? user)
        {
            var familyUsers = await _context.Family_Users
                            .Where(fu => fu.UserId == user.Id && fu.Is_manager)
                           .ToListAsync();

            List<string> familyNotEmpty = new List<string>();

            foreach (var familyUser in familyUsers)
            {
                var otherMember = _context.Family_Users
                    .FirstOrDefault(f => f.FamilyId == familyUser.FamilyId && f.UserId != familyUser.UserId);

                if (otherMember != null)
                {
                    familyNotEmpty.Add(familyUser.FamilyId);
                }
                else
                {
                    familyEmpty.Add(familyUser.FamilyId);
                }
            }

            return familyNotEmpty;
        }

        public async Task<IActionResult> OnGetNewManagerAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null)
            {
                return NotFound();
            }

            var families = await _context.Family_Users
            .Where(fu => fu.UserId == user.Id && fu.Is_manager)
            .Select(fu => new FamilleDTO()
            {
                Id = fu.FamilyId,
                Nom = fu.Family.Name,
                Users = fu.Family.Family_Users
                    .Where(familyUser => familyUser.UserId != user.Id)
                    .Select(familyUser => new UserDTO()
                    {
                        Id = familyUser.User.Id,
                        Nom = familyUser.User.UserName
                    }).ToList()
            })
            .Where(f => f.Users.Count > 0)
            .ToListAsync();

            return new JsonResult(families);
        }
        public async Task<IActionResult> OnPostDeactivateUserAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null)
            {
                return NotFound();
            }
            if (!user.IsActive)
            {
                return new JsonResult(new { success = false, message = "L'utilisateur est déjà désactivé." });
            }

            await GetFamiliesManager(user);
            if (familyEmpty.Count != 0)
            {
                var familiesToDelete = await _context.Families
                    .Where(f => familyEmpty.Contains(f.Id))
                    .ToListAsync();

                _context.Families.RemoveRange(familiesToDelete);

                await _context.SaveChangesAsync();
            }

            var userRelations = _context.Family_Users.Where(fu => fu.UserId == user.Id);
            _context.Family_Users.RemoveRange(userRelations);

            var userMemberShip = _context.MemberShip_Families.Where(fu => fu.RequesterId == user.Id);
            _context.MemberShip_Families.RemoveRange(userMemberShip);

            user.IsActive = false;
            await _userManager.UpdateAsync(user);

            return new JsonResult(new { success = true });
        }
        public async Task<IActionResult> OnPostChangeManagerAsync(string login, List<UserFamily> userFamilies)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            if (userFamilies == null || !userFamilies.Any())
            {
                return BadRequest("Aucune donnée de gestionnaire n'a été fournie.");
            }

            var newManagers = new List<(ApplicationUser Manager, string FamilyName)>();
            foreach (var userFamily in userFamilies)
            {
                var familyMembership = await _context.Family_Users
                    .FirstOrDefaultAsync(fu => fu.FamilyId == userFamily.FamilyId && fu.UserId == userFamily.UserId);

                if (familyMembership == null)
                {
                    return BadRequest($"L'utilisateur ID {userFamily.UserId} n'est pas un membre valide de la famille ID {userFamily.FamilyId}.");
                }

                var currentManager = await _context.Family_Users
                    .FirstOrDefaultAsync(fu => fu.FamilyId == userFamily.FamilyId && fu.Is_manager);

                if (currentManager != null)
                {
                    currentManager.Is_manager = false;
                }

                familyMembership.Is_manager = true;
                var manager = await _userManager.FindByIdAsync(userFamily.UserId);
                var familyName = await _context.Families
                    .Where(f => f.Id == userFamily.FamilyId)
                    .Select(f => f.Name)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(familyName) && manager != null)
                {
                    newManagers.Add((manager, familyName));
                }
            }
            await _context.SaveChangesAsync();

            foreach (var (manager, familyName) in newManagers)
            {
                await _emailSender.SendEmailAsync(
                    manager.Email,
                    "Vous êtes maintenant manager de l'équipe",
                    $"Bonjour {manager.UserName},<br><br>" +
                    $"Félicitations ! Vous êtes désormais le manager de l'équipe <strong>{familyName}</strong>.<br>" +
                    $"Nous vous remercions pour votre implication.<br><br>" +
                    $"Cordialement,<br>L'équipe."
                );
            }

            return new JsonResult(new { success = true });
        }
    }
}
