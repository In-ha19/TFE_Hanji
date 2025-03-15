using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.DTO.Admin.Articles;
using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire_Collections.Pages.Admin.Articles
{
    [Authorize(Roles = "Admin")]
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
                    Note = c.Note
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

            return Page();
        }
    }
}
