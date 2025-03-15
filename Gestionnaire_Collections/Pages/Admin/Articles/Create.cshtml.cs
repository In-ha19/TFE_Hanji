using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Principal;

namespace Gestionnaire_Collections.Pages.Admin.Articles
{
    [Authorize(Roles = "Admin")]
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

        [BindProperty(SupportsGet = true)]
        public string? CategoryPSearch { get; set; } = string.Empty;
        public List<SelectListItem>? CategoriesPrincipalList { get; set; } = new List<SelectListItem>();

        public IActionResult OnGet()
        {
            var categoriesPrincipales = _context.Categories
                .Where(c => c.Is_active && c.Is_principal)
                .ToList();
            ViewData["CategoriesPrincipal"] = new SelectList(categoriesPrincipales, "Id", "Name");

            var defaultCategoryPrincipalId = categoriesPrincipales.FirstOrDefault()?.Id;

            if (defaultCategoryPrincipalId != null)
            {
                var categoriesSecondaires = _context.Categories
                    .Where(c => c.Is_active && !c.Is_principal && c.Parent_id == defaultCategoryPrincipalId)
                    .ToList();
                ViewData["CategoriesSecondary"] = new SelectList(categoriesSecondaires, "Id", "Name");
            }
            else
            {
                ViewData["CategoriesSecondary"] = new SelectList(new List<Category>(), "Id", "Name");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
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

                Article.SelectedCategoryIdsSecondary = new List<string>();

                return Page();
            }

            var existingArticleName = await _context.Articles
                    .FirstOrDefaultAsync(a => a.Name.ToLower() == Article.Name.ToLower());

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

            if (!string.IsNullOrEmpty(Article.SelectedCategoryId))
            {
                var categoryIdP = Article.SelectedCategoryId;

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
