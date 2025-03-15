using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;
using Microsoft.AspNetCore.Identity;
using Gestionnaire_Collections.ViewModels;

namespace Gestionnaire_Collections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserArticles : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserArticles(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("GetUserArticles")]
        public async Task<IActionResult> GetUserArticles([FromBody] UserArticlesRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Login) || !request.Categories.Any())
            {
                return BadRequest("Données invalides.");
            }

            var user = await _userManager.FindByNameAsync(request.Login);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            var userId = user.Id;  
            var categories = request.Categories;

            var userCollectionArticleIds = await _context.Collections
                                                           .Where(c => c.UserId == userId)
                                                           .Select(c => c.ArticleId)
                                                           .ToListAsync();

            var articlesQuery = _context.Articles
                .Join(_context.Category_Articles,
                    a => a.Id,
                    ca => ca.ArticleId,
                    (a, ca) => new { a, ca })
                .Join(_context.Categories,
                    ac => ac.ca.CategoryId,
                    c => c.Id,
                    (ac, c) => new { ac.a, c })
                .Where(a => a.a.Is_active == true
                            && categories.Contains(a.c.Name)  
                            && userCollectionArticleIds.Contains(a.a.Id))  
                .Select(a => new CollectionViewModelIndex
                {
                    Id = a.a.Id,
                    Name = a.a.Name,
                    Edition = a.a.Edition,
                    Autor_name = a.a.Autor_name,
                    CategoryName = a.c.Name  
                });

            var articles = await articlesQuery.ToListAsync();

            if (!articles.Any())
            {
                return NotFound("Aucun article trouvé pour les catégories sélectionnées.");
            }

            return Ok(articles);
        }
    }

    public class UserArticlesRequest
    {
        public string Login { get; set; }
        public List<string> Categories { get; set; }
    }

    public class CollectionViewModelIndex
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Edition { get; set; }
        public string Autor_name { get; set; }
        public string CategoryName { get; set; }  
    }
}
