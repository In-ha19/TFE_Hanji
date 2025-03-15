using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArticlesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCollection([FromBody] AddBookToCollectionRequest request)
        {
            if (string.IsNullOrEmpty(request.Title) && string.IsNullOrEmpty(request.Isbn))
            {
                return BadRequest("Le titre ou l'ISBN de l'article est manquant.");
            }

            if (string.IsNullOrEmpty(request.Login))
            {
                return BadRequest("Login utilisateur requis.");
            }

            // Rechercher l'utilisateur par login
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.Login);
            if (user == null)
            {
                return BadRequest("Utilisateur non trouvé.");
            }
            string userId = user.Id;

            // Rechercher l'id de la catégorie
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == request.Category.ToLower());

            if (category == null)
            {
                return BadRequest("Catégorie non trouvée.");
            }

            string categoryId = category.Id;

            // Rechercher l'article par titre
            var existingArticle = await _context.Articles
                .FirstOrDefaultAsync(a => a.Name.ToLower() == request.Title.ToLower());

            if (existingArticle == null && !string.IsNullOrEmpty(request.Isbn))
            {
                // Si l'article n'existe pas par titre, chercher par ISBN
                existingArticle = await _context.Articles
                    .FirstOrDefaultAsync(a => a.Isbn.ToLower() == request.Isbn.ToLower());
            }

            if (existingArticle == null)
            {
                // Si aucun article trouvé, en créer un nouveau
                existingArticle = new Article
                {
                    Name = request.Title,
                    Isbn = request.Isbn,
                    Autor_name = request.Author,  // Si l'auteur est fourni
                    Edition = request.Edition,  // Si l'édition est fournie
                    Is_active = true,
                    
                };

                _context.Articles.Add(existingArticle);
                await _context.SaveChangesAsync();
            }

            var existingCategoryArticle = await _context.Category_Articles
                   .FirstOrDefaultAsync(ca => ca.ArticleId == existingArticle.Id && ca.CategoryId == categoryId);

            if (existingCategoryArticle == null)
            {
                var categoryArticleP = new Category_Article
                {
                    ArticleId = existingArticle.Id,
                    CategoryId = categoryId
                };

                _context.Category_Articles.Add(categoryArticleP);
                await _context.SaveChangesAsync();
            }

            // Vérifier si l'article est déjà dans la collection de l'utilisateur
            bool exists = await _context.Collections
                .AnyAsync(c => c.UserId == userId && c.ArticleId == existingArticle.Id);

            if (exists)
            {
                return new JsonResult(new { success = false, message = "L'article est déjà dans votre collection." });
            }

            // Ajouter l'article à la collection de l'utilisateur
            var newCollection = new Collection
            {
                UserId = userId,
                ArticleId = existingArticle.Id,
                Statut = 3,  // statut par défaut 3
            };

            _context.Collections.Add(newCollection);
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true, message = "Article ajouté à votre collection." });
        }

        public class AddBookToCollectionRequest
        {
            public string Title { get; set; }
            public string Isbn { get; set; }
            public string? Author { get; set; }
            public string? Edition { get; set; }
            public string Login { get; set; }
            public string Category { get; set; }
        }


    }

}
