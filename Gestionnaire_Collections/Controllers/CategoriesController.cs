using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;


namespace Gestionnaire_Collections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories
                .Where(c => c.Is_active && c.Is_principal)
                .Select(c => c.Name) 
                .ToListAsync();

            if (categories == null || categories.Count == 0)
            {
                return NotFound("Aucune catégorie trouvée.");
            }

            return Ok(categories);
        }
    }
}
