using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Shared.Families
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Family Family { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; } = string.Empty;
        public async Task<IActionResult> OnGetAsync(string id, string returnUrl)
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
            Family = family;

            ReturnUrl = returnUrl ?? "/Index";
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Family).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyExists(Family.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect(ReturnUrl);
        }

        private bool FamilyExists(string id)
        {
            return _context.Families.Any(e => e.Id == id);
        }
    }
}
