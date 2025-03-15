using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Gestionnaire_Collections.ViewModels;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using Gestionnaire_Collections.DTO.Shared.Collections;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Shared.Collections
{
    [IgnoreAntiforgeryToken]

    [Authorize]
    public class IndexFamilysCollectionModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexFamilysCollectionModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }

        public PaginatedList<CollectionViewModelIndex> Articles { get; set; } = null!;
        public List<string> UserCollectionArticleIds { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";
        public string SearchQuery { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            const int pageSize = 10;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(Name))
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name == Name && c.Is_active);

                if (category == null)
                {
                    ErrorMessage = "La catégorie demandée est introuvable ou désactivée.";
                    return;
                }

                var userFamilies = await _context.Family_Users
                    .Where(fu => fu.UserId == userId)
                    .Select(fu => fu.FamilyId)
                    .ToListAsync();

                var userIdsInFamilies = await _context.Family_Users
                    .Where(fu => userFamilies.Contains(fu.FamilyId))
                    .Select(fu => fu.UserId)
                    .Distinct()
                    .ToListAsync();

                var query = _context.Articles
                 .Include(a => a.Category_Articles)
                 .ThenInclude(ca => ca.Category)
                 .Where(a => a.Is_active &&
                             a.Category_Articles.Any(ca => ca.Category.Name == Name) &&
                             _context.Collections
                                 .Where(c => userIdsInFamilies.Contains(c.UserId)) 
                                 .Any(c => c.ArticleId == a.Id)) 
                 .Select(a => new CollectionViewModelIndex
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

                query = SortColumn switch
                {
                    "Name" => SortOrder == "asc" ? query.OrderBy(a => a.Name) : query.OrderByDescending(a => a.Name),
                    "Edition" => SortOrder == "asc" ? query.OrderBy(a => a.Edition) : query.OrderByDescending(a => a.Edition),
                    "Autor_name" => SortOrder == "asc" ? query.OrderBy(a => a.Autor_name) : query.OrderByDescending(a => a.Autor_name),
                    "Date" => SortOrder == "asc" ? query.OrderBy(a => a.Date) : query.OrderByDescending(a => a.Date),
                    _ => query.OrderBy(a => a.Name),
                };

                Articles = await PaginatedList<CollectionViewModelIndex>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);

            }
            else
            {
                ErrorMessage = "Erreur.";
            }
        }

        public async Task<bool> IsArticleInCollectionAsync(string userId, string articleId)
        {
            return await _context.Collections.AnyAsync(c => c.UserId == userId && c.ArticleId == articleId);
        }
    }
}
