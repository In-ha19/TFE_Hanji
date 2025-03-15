using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.ViewModels;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Admin.Categories
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Gestionnaire_Collections.Data.AppDbContext _context;

        public CreateModel(Gestionnaire_Collections.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var principalCategories = _context.Categories
                                      .Where(c => c.Is_principal == true)
                                      .Select(c => new { c.Id, c.Name })
                                      .ToList();

            ViewData["Parent_id"] = new SelectList(principalCategories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public CategoryViewModelCreate Category { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!Category.Is_principal && string.IsNullOrEmpty(Category.Parent_id))
            {
                ModelState.AddModelError("Category.Parent_id", "Veuillez sélectionner une catégorie principale.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var categoryEntity = new Category
            {
                Id = Category.Id,
                Name = Category.Name,
                Is_active = true,
                Is_principal = Category.Is_principal,
                Parent_id = string.IsNullOrEmpty(Category.Parent_id) ? null : Category.Parent_id
            };

            _context.Categories.Add(categoryEntity); 
            await _context.SaveChangesAsync(); 

            return RedirectToPage("./Index");
        }

    }
}
