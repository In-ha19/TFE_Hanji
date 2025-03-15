using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Admin.Categories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Gestionnaire_Collections.Data.AppDbContext _context;

        public IndexModel(Gestionnaire_Collections.Data.AppDbContext context)
        {
            _context = context;
        }
        public PaginatedList<CategoryViewModelIndex> Category { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";
        public string SearchQuery { get; set; }

        public async Task OnGetAsync(string searchString, string statusFilter, string typeFilter)
        {
            const int pageSize = 10;

            // Requête de base
            var categoriesQuery = _context.Categories
                .Include(c => c.Parent)
                .Select(c => new CategoryViewModelIndex
                {
                    Id = c.Id,
                    Name = c.Name,
                    Is_active = c.Is_active,
                    Is_principal = c.Is_principal,
                    Parent_id = c.Parent_id,
                    Parent_name = c.Parent != null ? c.Parent.Name : "Principale"
                });

            // Appliquer les filtres de recherche
            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearchString = searchString.ToLower();
                categoriesQuery = categoriesQuery.Where(a =>
                    a.Name.ToLower().Contains(normalizedSearchString) ||                   
                    a.Parent_name.ToLower().Contains(normalizedSearchString));
            }

            // Filtrer par statut (Actif/Inactif)
            if (!string.IsNullOrEmpty(statusFilter))
            {
                if (statusFilter == "active")
                {
                    categoriesQuery = categoriesQuery.Where(c => c.Is_active);
                }
                else if (statusFilter == "inactive")
                {
                    categoriesQuery = categoriesQuery.Where(c => !c.Is_active);
                }
            }

            // Filtrer par type (Principal/Secondaire)
            if (!string.IsNullOrEmpty(typeFilter))
            {
                if (typeFilter == "principal")
                {
                    categoriesQuery = categoriesQuery.Where(c => c.Is_principal);
                }
                else if (typeFilter == "secondary")
                {
                    categoriesQuery = categoriesQuery.Where(c => !c.Is_principal);
                }
            }

            // Tri dynamique
            categoriesQuery = SortColumn switch
            {
                "Name" => SortOrder == "desc" ? categoriesQuery.OrderByDescending(c => c.Name) : categoriesQuery.OrderBy(c => c.Name),
                "Parent_name" => SortOrder == "desc" ? categoriesQuery.OrderByDescending(c => c.Parent_name) : categoriesQuery.OrderBy(c => c.Parent_name),
                _ => categoriesQuery.OrderBy(c => c.Name) 
            };

            // Créer la liste paginée
            Category = await PaginatedList<CategoryViewModelIndex>.CreateAsync(categoriesQuery.AsNoTracking(), PageIndex, pageSize);
        }
        public async Task<IActionResult> OnPostStatutCategoryAsync(string categoryId)
        {
            var category = await _context.Categories
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                return NotFound();
            }

            bool newStatus = !category.Is_active;
            category.Is_active = newStatus;

            if (!newStatus && category.Is_principal)
            {
                var articlesLinked = await _context.Category_Articles
                    .AnyAsync(ca => ca.CategoryId == categoryId);

                if (articlesLinked)
                {
                    TempData["ErrorMessage"] = "Vous ne pouvez pas désactiver cette catégorie car des articles y sont liés.";
                    return RedirectToPage();
                }
                var subcategories = await _context.Categories
                    .Where(c => c.Parent_id == category.Id)
                    .ToListAsync();

                foreach (var subcategory in subcategories)
                {
                    subcategory.Is_active = false;
                }
            }
            else if (newStatus && !category.Is_principal)
            {
                var parentCategory = category.Parent;
                while (parentCategory != null)
                {
                    if (!parentCategory.Is_active)
                    {
                        parentCategory.Is_active = true;
                    }

                    parentCategory = await _context.Categories
                        .FirstOrDefaultAsync(c => c.Id == parentCategory.Parent_id);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
