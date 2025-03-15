using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Admin.Articles
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> MainCategories { get; set; }
        public List<Category> SubCategories { get; set; }
        public PaginatedList<ArticleViewModelIndex> Articles { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";

        [BindProperty(SupportsGet = true)]
        public string? MainCategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SubCategoryId { get; set; }

        public string SearchQuery { get; set; }

        public async Task OnGetAsync(string searchString, string statusFilter)
        {
            const int pageSize = 10;

            // Charger les catégories principales et secondaires
            MainCategories = await _context.Categories.Where(c => c.Is_principal).ToListAsync();
            SubCategories = await _context.Categories.Where(c => !c.Is_principal).ToListAsync();

            // Définir une requête de base pour les articles
            var query = _context.Articles
                .Include(a => a.Category_Articles)
                .ThenInclude(ca => ca.Category)
                .Select(a => new ArticleViewModelIndex
                {
                    Id = a.Id,
                    Name = a.Name,
                    Edition = a.Edition,
                    Autor_name = a.Autor_name,
                    Date = a.Date,
                    Is_active = a.Is_active,
                    Category_Articles = a.Category_Articles.ToList()
                });

            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearchString = searchString.ToLower();
                query = query.Where(a =>
                    a.Name.ToLower().Contains(normalizedSearchString) ||
                    a.Edition.ToLower().Contains(normalizedSearchString) ||
                    a.Autor_name.ToLower().Contains(normalizedSearchString));
            }

            if (!string.IsNullOrEmpty(MainCategoryId))
            {
                query = query.Where(a => a.Category_Articles.Any(ca => ca.CategoryId == MainCategoryId));
            }

            if (!string.IsNullOrEmpty(SubCategoryId))
            {
                query = query.Where(a => a.Category_Articles.Any(ca => ca.CategoryId == SubCategoryId));
            }

            query = SortColumn switch
            {
                "Name" => SortOrder == "asc" ? query.OrderBy(a => a.Name) : query.OrderByDescending(a => a.Name),
                "Edition" => SortOrder == "asc" ? query.OrderBy(a => a.Edition) : query.OrderByDescending(a => a.Edition),
                "Autor_name" => SortOrder == "asc" ? query.OrderBy(a => a.Autor_name) : query.OrderByDescending(a => a.Autor_name),
                "Date" => SortOrder == "asc" ? query.OrderBy(a => a.Date) : query.OrderByDescending(a => a.Date),
                _ => query.OrderBy(a => a.Name), 
            };

            Articles = await PaginatedList<ArticleViewModelIndex>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID de l'article manquant.");
            }

            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            var associatedImages = await _context.Images
                .Where(img => img.ArticleId == id)
                .ToListAsync();

            if (associatedImages.Any())
            {
                _context.Images.RemoveRange(associatedImages);
            }

            _context.Articles.Remove(article);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "L'article a été supprimé avec succès.";
            return RedirectToPage("./Index");
        }
    }
}
