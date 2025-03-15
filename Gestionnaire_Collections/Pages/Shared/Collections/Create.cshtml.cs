using Gestionnaire_Collections.DTO.Shared.Collections;
using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Gestionnaire_Collections.Pages.Shared.Collections
{
    [IgnoreAntiforgeryToken]

    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Gestionnaire_Collections.Data.AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CreateModel(Gestionnaire_Collections.Data.AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public ArticleViewModelCreate_Edit Article { get; set; } = new ArticleViewModelCreate_Edit();

        //public List<Category> Categories { get; set; } = new List<Category>();
        public List<Category> CategoriesPrincipal { get; set; } = new List<Category>();
        public List<Category> CategoriesSecondary { get; set; } = new List<Category>();
        public IFormFile? ImageFile { get; set; }
        public string CategoryName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CategoryPSearch { get; set; } = string.Empty;
        public List<SelectListItem>? CategoriesPrincipalList { get; set; } = new List<SelectListItem>();

        public IActionResult OnGet(string name)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.Name == name && c.Is_active);

            if (category != null)
            {
                CategoryName = category.Name;

                var categoriesSecondary = _context.Categories
                    .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == category.Id)
                    .ToList();

                ViewData["CategoriesSecondary"] = new SelectList(categoriesSecondary, "Id", "Name");
            }
            else
            {
                CategoryName = "Catégorie non trouvée";
                ViewData["CategoriesSecondary"] = new SelectList(new List<Category>(), "Id", "Name");
            }

            ViewData["CategoryName"] = CategoryName;

            ViewData["CategoriesPrincipal"] = new SelectList(_context.Categories
                .Where(c => c.Is_active && c.Is_principal)
                .ToList(), "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Name == name && c.Is_active);
            if (category != null)
            {
                CategoryName = category.Name;
            }
            else
            {
                CategoryName = "Catégorie non trouvée";
            }

            if (!ModelState.IsValid)
            {
                ViewData["CategoryName"] = CategoryName;
                ViewData["CategoriesPrincipal"] = new SelectList(_context.Categories.Where(c => c.Is_active && c.Is_principal).ToList(), "Id", "Name");

                ViewData["CategoriesSecondary"] = new SelectList(_context.Categories
                   .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == category.Id)
                     .ToList(), "Id", "Name");

                Article.SelectedCategoryIdsSecondary = new List<string>();

                return Page();
            }

            var existingArticleName = await _context.Articles
                    .FirstOrDefaultAsync(a => a.Name.ToLower() == Article.Name.ToLower());

            if (existingArticleName != null)
            {
                 ModelState.AddModelError("Article.Name", "Cet article existe déjà.");

                ViewData["CategoryName"] = CategoryName;
                ViewData["CategoriesPrincipal"] = new SelectList(_context.Categories.Where(c => c.Is_active && c.Is_principal).ToList(), "Id", "Name");

                ViewData["CategoriesSecondary"] = new SelectList(_context.Categories
                   .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == category.Id)
                     .ToList(), "Id", "Name");

                Article.SelectedCategoryIdsSecondary = new List<string>();

                return Page();
            }

            var existingArticleISBN = await _context.Articles
                .FirstOrDefaultAsync(a => (a.Isbn != null
                    && Article.Isbn != null
                    && a.Isbn.ToLower() == Article.Isbn.ToLower())
                    && a.Id != Article.Id);

            if (existingArticleISBN != null)
            {
                ModelState.AddModelError("Article.Isbn", "Cet ISBN existe déjà, veuillez vérifier votre entré.");

                ViewData["CategoryName"] = CategoryName;
                ViewData["CategoriesPrincipal"] = new SelectList(_context.Categories.Where(c => c.Is_active && c.Is_principal).ToList(), "Id", "Name");

                ViewData["CategoriesSecondary"] = new SelectList(_context.Categories
                   .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == category.Id)
                     .ToList(), "Id", "Name");

                Article.SelectedCategoryIdsSecondary = new List<string>();

                return Page();
            }

            float? price = 0;

            if (!string.IsNullOrWhiteSpace(Article.Price))
            {
                var remplacedPrice = Article.Price.Replace(',', '.');

                if (!float.TryParse(remplacedPrice, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedPrice))
                {
                    ModelState.AddModelError("Article.Price", "Le prix doit être un nombre valide.");

                    ViewData["CategoryName"] = CategoryName;
                    ViewData["CategoriesPrincipal"] = new SelectList(_context.Categories.Where(c => c.Is_active && c.Is_principal).ToList(), "Id", "Name");

                    ViewData["CategoriesSecondary"] = new SelectList(_context.Categories
                       .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == category.Id)
                         .ToList(), "Id", "Name");

                    Article.SelectedCategoryIdsSecondary = new List<string>();

                    return Page();
                }
                price = parsedPrice;
            }
            else
            {
                price = null;
            }
            var articleEntity = new Article
            {
                Name = Article.Name,
                Is_active = true,
                Edition = Article.Edition,
                Isbn = Article.Isbn,
                Autor_name = Article.Autor_name,
                Date = Article.Date,
                Summary = Article.Summary,
                Price = price,
                //ImageId = image.Id,
            };

            _context.Articles.Add(articleEntity);
            await _context.SaveChangesAsync();

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var imageType = Path.GetExtension(fileName);
                var imagePath = Path.Combine(_env.WebRootPath, "images", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                var image = new Image
                {
                    Type = imageType,
                    Root = $"/images/{fileName}",
                    ArticleId = articleEntity.Id,
                };

                _context.Images.Add(image);
                await _context.SaveChangesAsync();

                //Article.ImageId = image.Id.ToString();
            }
                var categoryIdP = category.Id;

                var existingCategoryArticle = await _context.Category_Articles
                    .FirstOrDefaultAsync(ca => ca.ArticleId == articleEntity.Id && ca.CategoryId == categoryIdP);

                if (existingCategoryArticle == null)
                {
                    var categoryArticleP = new Category_Article
                    {
                        ArticleId = articleEntity.Id,
                        CategoryId = categoryIdP
                    };

                    _context.Category_Articles.Add(categoryArticleP);
                }

            if (Article.SelectedCategoryIdsSecondary != null && Article.SelectedCategoryIdsSecondary.Any())
            {
                foreach (var categoryIdS in Article.SelectedCategoryIdsSecondary)
                {
                    var categoryArticleS = new Category_Article
                    {
                        ArticleId = articleEntity.Id,
                        CategoryId = categoryIdS
                    };

                    _context.Category_Articles.Add(categoryArticleS);
                }
            }
            await _context.SaveChangesAsync();

            TempData["ArticleId"] = articleEntity.Id;
            TempData["ShowModal"] = true;

            //return RedirectToPage("./Index");
            ViewData["CategoryName"] = CategoryName;
            ViewData["CategoriesPrincipal"] = new SelectList(_context.Categories.Where(c => c.Is_active && c.Is_principal).ToList(), "Id", "Name");

            ViewData["CategoriesSecondary"] = new SelectList(_context.Categories
               .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == category.Id)
                 .ToList(), "Id", "Name");

            Article.SelectedCategoryIdsSecondary = new List<string>();

            return RedirectToPage("Create", new { name=name});
        }
        public async Task<IActionResult> OnGetSearchAsync(string CategorySearch, bool isPrincipal, string categoryName, List<string> selectedCategoryIds)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CategorySearch))
                {
                    return new JsonResult(new List<SelectListItem>());
                }

                var principalCategory = await _context.Categories
                     .Where(c => c.Name == categoryName && c.Is_principal == true)
                     .FirstOrDefaultAsync();

                if (principalCategory == null)
                {
                    return new JsonResult(new List<SelectListItem>());
                }

                var categories = await _context.Categories
                    .Where(c => c.Is_active
                            && c.Is_principal == false 
                            && c.Parent_id == principalCategory.Id 
                            && !selectedCategoryIds.Contains(c.Id.ToString()) 
                            && EF.Functions.Like(c.Name, $"%{CategorySearch}%"))
                    .Select(c => new { value = c.Id, text = c.Name })
                    .ToListAsync();

                return new JsonResult(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> OnGetAllCategoriesSecondaryAsync([FromQuery] List<string> excludeIds, string categoryName)
        {
            var CategoryId = "";
            var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName && c.Is_active);
            if (category != null)
            {
                CategoryId = category.Id;  
            }
            else
            {
                CategoryId = null;  
            }

            var categories = await _context.Categories
                .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == CategoryId &&  (excludeIds == null || !excludeIds.Contains(c.Id)))
                .Select(c => new { value = c.Id, text = c.Name })
                .ToListAsync();

            return new JsonResult(categories);
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
    }
}
