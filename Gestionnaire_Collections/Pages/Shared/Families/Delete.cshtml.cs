using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Shared.Families
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly Gestionnaire_Collections.Data.AppDbContext _context;

        public DeleteModel(Gestionnaire_Collections.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Family Family { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Families.FirstOrDefaultAsync(m => m.Id == id);

            if (family == null)
            {
                return NotFound();
            }
            else
            {
                Family = family;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Families.FindAsync(id);
            if (family != null)
            {
                var familyUsers = await _context.Family_Users.Where(fu => fu.FamilyId == id).ToListAsync();
                _context.Family_Users.RemoveRange(familyUsers);

                var membershipFamilies = await _context.MemberShip_Families.Where(mf => mf.FamilyId == id).ToListAsync();
                _context.MemberShip_Families.RemoveRange(membershipFamilies);

                _context.Families.Remove(family);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./IndexMyFamilies");
        }
    }
}
