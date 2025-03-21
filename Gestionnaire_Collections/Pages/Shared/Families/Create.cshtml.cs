using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire_Collections.Pages.Shared.Families
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Family Family { get; set; } = new Family();

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingFamilyName = await _context.Families
                    .FirstOrDefaultAsync(a => a.Name.ToLower() == Family.Name.ToLower());

            if (existingFamilyName != null)
            {
                ModelState.AddModelError("Family.Name", "Cette famille existe déjà, veuillez choisir un autre nom.");

                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            Family.Id = Guid.NewGuid().ToString(); 
            _context.Families.Add(Family);
            await _context.SaveChangesAsync();

            var familyUser = new Family_User
            {
                FamilyId = Family.Id,
                UserId = user.Id,
                Is_manager = true 
            };

            _context.Family_Users.Add(familyUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
