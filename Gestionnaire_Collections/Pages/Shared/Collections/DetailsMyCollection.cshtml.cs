using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Shared.Collections
{
    [Authorize]
    public class EditMyCollectionModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditMyCollectionModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public CollectionViewModelDetailsMyCollection Collection { get; set; }

        public string CurrentUserId { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login"); 
            }
            CurrentUserId = currentUser.Id;

            Collection = await _context.Collections
                .Where(c => c.ArticleId == id && c.UserId == CurrentUserId)  
                .Include(c => c.Article)
                .Select(c => new CollectionViewModelDetailsMyCollection
                {
                    Note = c.Note,
                    Statut = c.Statut,
                    Text = c.Text,
                    Purchased_Price = c.Purchased_Price,
                    Purchased_Date = c.Purchased_Date,
                })
                .FirstOrDefaultAsync();  

            if (Collection == null)
            {
                return NotFound(); 
            }

            var articleName = await _context.Articles
                 .Where(a => a.Id == id)
                 .Select(a => a.Name)  
                 .FirstOrDefaultAsync();

            if (articleName == null)
            {
                return NotFound(); 
            }

            var article = await _context.Articles
                .Include(a => a.Category_Articles)
                .ThenInclude(ca => ca.Category)
                .FirstOrDefaultAsync(a => a.Id == id);

            var mainCategory = article.Category_Articles
                .Select(ca => ca.Category)
                .FirstOrDefault(c => c.Is_principal);

            string categoryName = mainCategory?.Name ?? "Aucune catégorie principale";
            ViewData["CategoryName"] = categoryName;

            ViewData["ArticleName"] = articleName;
            ViewData["ArticleId"] = id;

            return Page(); 
        }

        public async Task<IActionResult> OnPostEditCollection()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Données invalides." })
                {
                    StatusCode = 400
                };
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return new JsonResult(new { success = false, message = "Utilisateur non connecté." })
                {
                    StatusCode = 401 
                };
            }

            var articleId = Request.Form["ArticleId"].ToString();

            var collectionToUpdate = await _context.Collections
                .FirstOrDefaultAsync(c => c.ArticleId == articleId && c.UserId == currentUser.Id);

            if (collectionToUpdate == null)
            {
                return new JsonResult(new { success = false, message = "La collection n'existe pas ou n'appartient pas à l'utilisateur." })
                {
                    StatusCode = 404
                };
            }

            collectionToUpdate.Note = Collection.Note;
            collectionToUpdate.Statut = Collection.Statut;
            collectionToUpdate.Text = Collection.Text;
            collectionToUpdate.Purchased_Price = Collection.Purchased_Price;
            collectionToUpdate.Purchased_Date = Collection.Purchased_Date;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Modifications enregistrées avec succès !" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Erreur lors de l'enregistrement : {ex.Message}" })
                {
                    StatusCode = 500
                };
            }
        }
    }
}
