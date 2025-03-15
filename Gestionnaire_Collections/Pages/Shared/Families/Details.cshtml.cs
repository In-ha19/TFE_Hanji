using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Gestionnaire_Collections.Pages.Shared.Families
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Family Family { get; set; }
        public List<ApplicationUser> FamilyMembers { get; set; } = new List<ApplicationUser>();
        public List<MemberShip_Family> Requests { get; set; } = new List<MemberShip_Family>();
        public List<Family_User> FamilyUsers { get; set; } = new List<Family_User>();

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string id, string returnUrl = "IndexMyFamilies")
        {
            if (id == null)
            {
                return NotFound();
            }

            Family = await _context.Families
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Family == null)
            {
                return NotFound();
            }

            FamilyUsers = await _context.Family_Users
                .Where(fu => fu.FamilyId == id)
                .Include(fu => fu.User)
                .ToListAsync();

            FamilyMembers = FamilyUsers.Select(fu => fu.User).ToList();

            Requests = await _context.MemberShip_Families
                .Where(mf => mf.FamilyId == id && mf.Statut == 0) 
                .Include(mf => mf.Requester)
                .ToListAsync();

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateRequestStatusAsync(int requestId, int status)
        {
            var request = await _context.MemberShip_Families.FindAsync(requestId);
            if (request == null)
            {
                return NotFound();
            }            
            if (status == 1)
            {
                request.Statut = status;
                _context.MemberShip_Families.Update(request);

                var familyUser = new Family_User
                {
                    FamilyId = request.FamilyId,
                    UserId = request.RequesterId,
                    Is_manager = false
                };

                _context.Family_Users.Add(familyUser);
            }
            else if (status == 2) // Refuser la demande
            {              
                _context.MemberShip_Families.Remove(request);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = request.FamilyId });
        }
        public async Task<IActionResult> OnPostPromoteToManagerAsync(string userId, string familyId)
        {
            var currentManager = await _context.Family_Users
                .FirstOrDefaultAsync(fu => fu.FamilyId == familyId && fu.Is_manager);

            if (currentManager != null && currentManager.UserId != userId)
            {
                currentManager.Is_manager = false;
                _context.Family_Users.Update(currentManager);
            }

            var newManager = await _context.Family_Users
                .FirstOrDefaultAsync(fu => fu.UserId == userId && fu.FamilyId == familyId);
            if (newManager != null)
            {
                newManager.Is_manager = true;
                _context.Family_Users.Update(newManager);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = familyId });
        }
        public async Task<IActionResult> OnPostRemoveUserAsync(string userId, string familyId)
        {
            var familyUser = await _context.Family_Users
                .FirstOrDefaultAsync(fu => fu.UserId == userId && fu.FamilyId == familyId);

            var membership = await _context.MemberShip_Families
                .FirstOrDefaultAsync(mf => mf.RequesterId == userId && mf.FamilyId == familyId);

            if (familyUser != null)
            {
                _context.Family_Users.Remove(familyUser);
            }

            if (membership != null)
            {
                _context.MemberShip_Families.Remove(membership);
            }
           
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = familyId });
        }

    }
}
