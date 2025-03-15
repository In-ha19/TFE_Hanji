using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.DTO.Admin.Articles;
using Gestionnaire_Collections.DTO.Shared.Collections;
using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gestionnaire_Collections.Pages.Shared.Collections
{
    [IgnoreAntiforgeryToken]

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

        public ArticleViewModelDetails Article { get; set; } = default!;
        public string EvaluationMessage { get; set; } = string.Empty;
        public List<string> UserCollectionArticleIds { get; set; }
        public string? ErrorMessage { get; set; }
        public List<ApplicationUser> FamilyUsersWithArticle { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category_Articles)
                .ThenInclude(ca => ca.Category)
                .Include(a => a.Collections)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .FirstOrDefaultAsync(i => i.ArticleId == article.Id);

            Article = new ArticleViewModelDetails
            {
                Id = article.Id,
                Name = article.Name,
                Edition = article.Edition,
                Isbn = article.Isbn,
                Autor_name = article.Autor_name,
                Date = article.Date,
                Summary = article.Summary,
                Is_active = article.Is_active,
                Price = article.Price,
                SelectedCategoryName = article.Category_Articles
                    .FirstOrDefault(ca => ca.Category.Is_principal)?
                    .Category.Name,
                SelectedCategoryNameSecondary = article.Category_Articles
                    .Where(ca => !ca.Category.Is_principal)
                    .Select(ca => ca.Category.Name) 
                    .ToList(),

                Collections = article.Collections.Select(c => new CollectionDTO
                {
                    UserId = c.UserId,
                    User = c.User,
                    ArticleId = c.ArticleId,
                    Note = c.Note,
                    Statut = c.Statut,  
                    Text = c.Text,
                    Purchased_Date = c.Purchased_Date,
                    Purchased_Price = c.Purchased_Price

                }).ToList(),
                ImageRoot = image?.Root  
            };

            var notes = article.Collections
                .Where(c => c.Note.HasValue)
                .Select(c => c.Note.Value)
                .ToList();

            EvaluationMessage = notes.Any()
                ? $"Moyenne : {notes.Average():F2}"
                : "Aucune évaluation disponible";

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userCollectionArticleIds = await _context.Collections
                .Where(c => c.UserId == userId)
                .Select(c => c.ArticleId)
                .ToListAsync();

            UserCollectionArticleIds = userCollectionArticleIds;

            return Page();
        }
        public async Task<IActionResult> OnPostGetFamilyUsersWithArticle(string articleId)
        {
            if (string.IsNullOrEmpty(articleId))
            {
                return new JsonResult(new { success = false, message = "ID d'article invalide." });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userFamilies = await _context.Family_Users
                .Where(fu => fu.UserId == userId)
                .Select(fu => fu.FamilyId)
                .ToListAsync();

            var usersWithArticleInCommonFamily = await _context.Family_Users
          .Where(fu => userFamilies.Contains(fu.FamilyId) &&
                       _context.Collections.Any(c => c.ArticleId == articleId && c.UserId == fu.UserId))
          .Select(fu => new
          {
              UserId = fu.UserId,
              UserName = fu.User.UserName,  
              FamilyName = fu.Family.Name  
          })
          .ToListAsync();

            var distinctUsers = usersWithArticleInCommonFamily
                                .GroupBy(u => u.UserId)
                                .Select(g => g.First())  
                                .ToList();

            if (distinctUsers.Any())
            {
                return new JsonResult(new
                {
                    success = true,
                    users = distinctUsers
                });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Aucun utilisateur trouvé avec cet article dans une famille commune." });
            }
        }
        public async Task<IActionResult> OnPostAddToCollectionAsync([FromForm] SharedCollectionDTO collection)
        {
            if (string.IsNullOrEmpty(collection.ArticleId) || string.IsNullOrEmpty(collection.UserId))
            {
                return BadRequest("L'identifiant de l'article ou de l'utilisateur est manquant.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest("Utilisateur non authentifié.");
            }

            collection.UserId = userId;

            bool exists = await _context.Collections
            .AnyAsync(c => c.UserId == userId && c.ArticleId == collection.ArticleId);

            if (exists)
            {
                return new JsonResult(new { success = false, message = "L'article est déjà dans votre collection." });
            }

            float price = 0;
            if (!string.IsNullOrEmpty(collection.Purchased_Price))
            {
                collection.Purchased_Price = collection.Purchased_Price.Replace('.', ',');

                bool parsed = float.TryParse(collection.Purchased_Price, out price);
                if (!parsed)
                {
                    return new JsonResult(new { success = false, message = "Le prix d'achat est invalide." });
                }
            }

            var newCollection = new Collection
            {
                UserId = collection.UserId,
                ArticleId = collection.ArticleId,
                Purchased_Price = price,
                Purchased_Date = collection.Purchased_Date,
                Note = collection.Note,
                Statut = collection.Statut,
                Text = collection.Text
            };

            _context.Collections.Add(newCollection);
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }
        public async Task<bool> IsArticleInCollectionAsync(string userId, string articleId)
        {
            return await _context.Collections.AnyAsync(c => c.UserId == userId && c.ArticleId == articleId);
        }
    }
}
