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
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Admin.Categories
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly Gestionnaire_Collections.Data.AppDbContext _context;

        public EditModel(Gestionnaire_Collections.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CategoryViewModelEdit Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category =  await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            Category = new CategoryViewModelEdit
            {
                Id = category.Id,
                Name = category.Name,
                Is_active = category.Is_active,
                Is_principal = category.Is_principal,
                Parent_id = category.Parent_id
            };

            var principalCategories = await _context.Categories
            .Where(c => c.Is_principal)
            .Select(c => new { c.Id, c.Name })
            .ToListAsync();

            ViewData["Parent_id"] = new SelectList(principalCategories, "Id", "Name");

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

            var category = await _context.Categories
            .Include(c => c.SubCategories) 
            .FirstOrDefaultAsync(c => c.Id == Category.Id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = Category.Name;
            category.Is_active = Category.Is_active;

            if (category.Is_principal)
            {
                if (!Category.Is_active)
                {
                    var articlesLinked = await _context.Category_Articles
                        .AnyAsync(ca => ca.CategoryId == category.Id);

                    if (articlesLinked)
                    {
                        TempData["ErrorMessage"] = "Vous ne pouvez pas désactiver cette catégorie car des articles y sont liés.";
                        return Page();
                    }

                    foreach (var subcategory in category.SubCategories)
                    {
                        subcategory.Is_active = false;
                    }
                }
            }
            else
            {
                category.Parent_id = Category.Parent_id;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(string id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
