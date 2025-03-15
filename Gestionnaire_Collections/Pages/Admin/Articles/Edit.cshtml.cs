using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Gestionnaire_Collections.Pages.Admin.Articles
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly Gestionnaire_Collections.Data.AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(Gestionnaire_Collections.Data.AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public ArticleViewModelCreate_Edit Article { get; set; } = new ArticleViewModelCreate_Edit();
        public List<Category> CategoriesPrincipal { get; set; } = new List<Category>();
        public List<Category> CategoriesSecondary { get; set; } = new List<Category>();
        public IFormFile? ImageFile { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? CategoryPSearch { get; set; } = string.Empty;
        public List<SelectListItem>? CategoriesPrincipalList { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var article = await _context.Articles
                .Include(a => a.Category_Articles)
                .ThenInclude(ca => ca.Category)              
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            var image = await _context.Images
               .FirstOrDefaultAsync(i => i.ArticleId == article.Id);

            Article = new ArticleViewModelCreate_Edit
            {
                Id = article.Id,
                Name = article.Name,
                Autor_name = article.Autor_name,
                Isbn = article.Isbn,
                Summary = article.Summary,
                Edition = article.Edition,
                Date = article.Date,
                Price = article.Price?.ToString(CultureInfo.InvariantCulture),
                SelectedCategoryId = article.Category_Articles
                    .FirstOrDefault(ca => ca.Category.Is_principal)?.CategoryId,
                SelectedCategoryIdsSecondary = article.Category_Articles
                    .Where(ca => !ca.Category.Is_principal)
                    .Select(ca => ca.CategoryId)
                    .ToList(),
                ImageRoot = image?.Root
            };

            var categoriesPrincipal = await _context.Categories
                .Where(c => c.Is_active && c.Is_principal)
                .ToListAsync();



            var selectedCategoryId = Article.SelectedCategoryId;
            var categoriesSecondary = new List<Category>();

            if (!string.IsNullOrEmpty(selectedCategoryId))
            {
                categoriesSecondary = await _context.Categories
                    .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == selectedCategoryId)
                    .ToListAsync();
            }

            CategoriesPrincipal = categoriesPrincipal;
            CategoriesSecondary = categoriesSecondary;

            ViewData["CategoriesPrincipal"] = new SelectList(categoriesPrincipal, "Id", "Name");
            ViewData["CategoriesSecondary"] = new SelectList(categoriesSecondary, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Article.SelectedCategoryId))
                {
                    var categoryIdPrincipal = Article.SelectedCategoryId;

                    var categoriesSecondaires = _context.Categories
                        .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == categoryIdPrincipal)
                        .ToList();
                    ViewData["CategoriesSecondary"] = new SelectList(categoriesSecondaires, "Id", "Name");
                }
                else
                {
                    ViewData["CategoriesSecondary"] = new SelectList(new List<Category>(), "Id", "Name");
                }

                return Page();
            }
            var articleEntity = await _context.Articles
                .Include(a => a.Category_Articles)
                .ThenInclude(ca => ca.Category)
                .FirstOrDefaultAsync(a => a.Id == Article.Id);

            if (articleEntity == null)
            {
                return NotFound();
            }

            var existingArticleName = await _context.Articles
                    .FirstOrDefaultAsync(a => a.Name.ToLower() == Article.Name.ToLower() && a.Id != Article.Id);

            if (existingArticleName != null)
            {
                ModelState.AddModelError("Article.Name", "Cet article existe déjà.");
                ViewData["CategoriesPrincipal"] = new SelectList(_context.Categories.Where(c => c.Is_active && c.Is_principal).ToList(), "Id", "Name");
                if (!string.IsNullOrEmpty(Article.SelectedCategoryId))
                {
                    var categoryIdPrincipal = Article.SelectedCategoryId;

                    var categoriesSecondaires = _context.Categories
                        .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == categoryIdPrincipal)
                        .ToList();
                    ViewData["CategoriesSecondary"] = new SelectList(categoriesSecondaires, "Id", "Name");
                }
                else
                {
                    ViewData["CategoriesSecondary"] = new SelectList(new List<Category>(), "Id", "Name");
                }

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

                ViewData["CategoriesPrincipal"] = new SelectList(_context.Categories.Where(c => c.Is_active && c.Is_principal).ToList(), "Id", "Name");
                if (!string.IsNullOrEmpty(Article.SelectedCategoryId))
                {
                    var categoryIdPrincipal = Article.SelectedCategoryId;

                    var categoriesSecondaires = _context.Categories
                        .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == categoryIdPrincipal)
                        .ToList();
                    ViewData["CategoriesSecondary"] = new SelectList(categoriesSecondaires, "Id", "Name");
                }
                else
                {
                    ViewData["CategoriesSecondary"] = new SelectList(new List<Category>(), "Id", "Name");
                }

                return Page();
            }

            float? price = 0;

            if (!string.IsNullOrWhiteSpace(Article.Price))
            {
                var remplacedPrice = Article.Price.Replace(',', '.');

                if (!float.TryParse(remplacedPrice, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedPrice))
                {
                    ModelState.AddModelError("Article.Price", "Le prix doit être un nombre valide.");

                    ViewData["CategoriesPrincipal"] = new SelectList(_context.Categories.Where(c => c.Is_active && c.Is_principal).ToList(), "Id", "Name");
                    if (!string.IsNullOrEmpty(Article.SelectedCategoryId))
                    {
                        var categoryIdPrincipal = Article.SelectedCategoryId;

                        var categoriesSecondaires = _context.Categories
                            .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == categoryIdPrincipal)
                            .ToList();
                        ViewData["CategoriesSecondary"] = new SelectList(categoriesSecondaires, "Id", "Name");
                    }
                    else
                    {
                        ViewData["CategoriesSecondary"] = new SelectList(new List<Category>(), "Id", "Name");
                    }

                    return Page();

                }
                price = parsedPrice;
            }
            else
            {
                price = null;
            }

            articleEntity.Name = Article.Name;
            articleEntity.Is_active = true;
            articleEntity.Edition = Article.Edition;
            articleEntity.Isbn = Article.Isbn;
            articleEntity.Autor_name = Article.Autor_name;
            articleEntity.Date = Article.Date;
            articleEntity.Summary = Article.Summary;
            articleEntity.Price = price;
            
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var imageType = Path.GetExtension(fileName);
                var imagePath = Path.Combine(_env.WebRootPath, "images", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                var existingImage = await _context.Images.FirstOrDefaultAsync(i => i.ArticleId == articleEntity.Id);
                if (existingImage != null)
                {
                    _context.Images.Remove(existingImage);
                }

                var image = new Image
                {
                    Type = imageType,
                    Root = $"/images/{fileName}",
                    ArticleId = articleEntity.Id,
                };

                _context.Images.Add(image);               
            }

            if (!string.IsNullOrEmpty(Article.SelectedCategoryId))
            {
                var categoryIdP = Article.SelectedCategoryId;

                var existingCategoryArticlePrincipal = await _context.Category_Articles
                    .Include(ca => ca.Category) 
                    .FirstOrDefaultAsync(ca => ca.ArticleId == articleEntity.Id && ca.Category.Is_principal);

                if (existingCategoryArticlePrincipal != null && existingCategoryArticlePrincipal.CategoryId != categoryIdP)
                {
                    _context.Category_Articles.Remove(existingCategoryArticlePrincipal);
                }

                var newCategoryArticlePrincipal = await _context.Category_Articles
                    .FirstOrDefaultAsync(ca => ca.ArticleId == articleEntity.Id && ca.CategoryId == categoryIdP);

                if (newCategoryArticlePrincipal == null)
                {
                    var categoryArticleP = new Category_Article
                    {
                        ArticleId = articleEntity.Id,
                        CategoryId = categoryIdP
                    };

                    _context.Category_Articles.Add(categoryArticleP);
                }
            }       

            var existingCategoryIdsSecondary = articleEntity.Category_Articles
                .Where(ca => ca.Category != null && !ca.Category.Is_principal) 
                .Select(ca => ca.CategoryId)
                .ToList();

            var newCategoryIdsSecondary = Article.SelectedCategoryIdsSecondary ?? new List<string>();


            foreach (var categoryIdS in existingCategoryIdsSecondary.Except(newCategoryIdsSecondary))
            {
                var categoryToRemove = articleEntity.Category_Articles
                    .FirstOrDefault(ca => ca.CategoryId == categoryIdS);

                if (categoryToRemove != null)
                {
                    _context.Category_Articles.Remove(categoryToRemove);
                }
            }

            foreach (var categoryIdS in newCategoryIdsSecondary.Except(existingCategoryIdsSecondary))
            {
                var categoryArticleS = new Category_Article
                {
                    ArticleId = articleEntity.Id,
                    CategoryId = categoryIdS
                };

                _context.Category_Articles.Add(categoryArticleS);
            }
            /* if (Article.SelectedCategoryIdsSecondary != null && Article.SelectedCategoryIdsSecondary.Any())
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

                 await _context.SaveChangesAsync();
             }*/

            await _context.SaveChangesAsync();       

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetSearchAsync(string CategorySearch, bool isPrincipal, string categoryPrincipalId, List<string> selectedCategoryIds)
        {
            try
            {
                IQueryable<Category> categoriesQuery = _context.Categories.Where(c => c.Is_active && c.Is_principal == isPrincipal);

                if (!isPrincipal)
                {
                    categoriesQuery = categoriesQuery.Where(c => c.Parent_id == categoryPrincipalId);
                }

                if (selectedCategoryIds != null && selectedCategoryIds.Any())
                {
                    categoriesQuery = categoriesQuery.Where(c => !selectedCategoryIds.Contains(c.Id));
                }

                var categories = await categoriesQuery
                    .Where(c => string.IsNullOrWhiteSpace(CategorySearch) || EF.Functions.Like(c.Name, $"%{CategorySearch}%"))
                    .Select(c => new { value = c.Id, text = c.Name })
                    .ToListAsync();

                return new JsonResult(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        public async Task<IActionResult> OnGetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                .Where(c => c.Is_principal && c.Is_active)
                .Select(c => new { value = c.Id, text = c.Name })
                .ToListAsync();

            return new JsonResult(categories);
        }
        /*public async Task<IActionResult> OnGetAllCategoriesSecondaryAsync()
         {
             var categories = await _context.Categories
                 .Where(c => c.Is_active && !c.Is_principal)
                 .Select(c => new { value = c.Id, text = c.Name })
                 .ToListAsync();

             return new JsonResult(categories);
         }*/
        public async Task<IActionResult> OnGetSecondaryCategoriesAsync(string categoryPrincipalId)
        {
            if (string.IsNullOrEmpty(categoryPrincipalId))
            {
                return new JsonResult(new List<SelectListItem>());
            }

            var secondaryCategories = await _context.Categories
                .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == categoryPrincipalId)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            return new JsonResult(secondaryCategories);
        }

        public async Task<IActionResult> OnGetAllCategoriesSecondaryAsync(List<string> excludeIds, string categoryPrincipalId)
        {
            try
            {
                IQueryable<Category> categoriesQuery = _context.Categories
                    .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == categoryPrincipalId);

                if (excludeIds != null && excludeIds.Any())
                {
                    categoriesQuery = categoriesQuery.Where(c => !excludeIds.Contains(c.Id));
                }

                var categories = await categoriesQuery
                    .Select(c => new { value = c.Id, text = c.Name })
                    .ToListAsync();

                return new JsonResult(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
