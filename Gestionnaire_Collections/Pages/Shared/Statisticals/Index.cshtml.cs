using Gestionnaire_Collections.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestionnaire_Collections.DTO.Shared.Statisticals;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Shared.Statisticals
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public decimal TotalExpenses { get; set; }
        public List<CategoryExpense> CategoryExpenses { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        //[BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        //[BindProperty(SupportsGet = true)]
        public string Category { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public SelectList ActiveCategories { get; set; }
        public string SelectedCategory { get; set; }
        public decimal TotalCollectionPrice { get; set; }
        public int? SelectedYear { get; set; }
        public int? SelectedMonth { get; set; }
        public List<decimal> MonthlyExpenses { get; set; } = new();
        public decimal TotalExpensesFiltered { get; set; }
        public List<CategoryExpense> CategoryExpensesFiltered { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";

        [BindProperty(SupportsGet = true)]
        public string? SortColumn2 { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder2 { get; set; } = "asc";
        public string SearchQuery { get; set; }

        public async Task OnGetAsync(string searchString1, string searchString2, int? year = null, int? month = null)
        {
            var userId = _userManager.GetUserId(User);

            year ??= DateTime.Now.Year;
            month ??= DateTime.Now.Month;

            SelectedYear = year.Value;
            SelectedMonth = month.Value;

            TotalCollectionPrice = await _context.Collections
                .Where(c => c.UserId == userId)
                .SumAsync(c => (decimal?)c.Purchased_Price) ?? 0;

            var baseQuery = _context.Collections
                .Where(c => c.UserId == userId);

            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                baseQuery = baseQuery.Where(c => c.Article.Category_Articles
                    .Any(ca => ca.CategoryId == SelectedCategory));
            }

            if (SelectedMonth > 0)
            {
                baseQuery = baseQuery.Where(c => c.Purchased_Date.HasValue && c.Purchased_Date.Value.Month == SelectedMonth);
            }

            if (SelectedYear > 0)
            {
                baseQuery = baseQuery.Where(c => c.Purchased_Date.HasValue && c.Purchased_Date.Value.Year == SelectedYear);
            }

            TotalExpensesFiltered = await baseQuery.SumAsync(c => (decimal?)c.Purchased_Price) ?? 0;

            // Filtrage des dépenses par catégorie pour la période sélectionnée
            CategoryExpensesFiltered = await _context.Collections
                .Where(c => c.UserId == userId)
                .Where(c => c.Article.Category_Articles
                    .Any(ca => ca.Category.Is_principal && !string.IsNullOrEmpty(ca.Category.Name)))
                .Where(c => (!SelectedMonth.HasValue || c.Purchased_Date.HasValue && c.Purchased_Date.Value.Month == SelectedMonth) &&
                            (!SelectedYear.HasValue || c.Purchased_Date.HasValue && c.Purchased_Date.Value.Year == SelectedYear))
                .Where(c => string.IsNullOrEmpty(searchString1) || c.Article.Category_Articles
                    .Any(ca => ca.Category.Is_principal && ca.Category.Name.Contains(searchString1)))
                .GroupBy(c => c.Article.Category_Articles
                    .FirstOrDefault(ca => ca.Category.Is_principal).Category.Name)
                .Select(g => new CategoryExpense
                {
                    CategoryName = g.Key,
                    TotalSpent = g.Sum(c => (decimal?)c.Purchased_Price) ?? 0
                })
                .ToListAsync();


            CategoryExpenses = await _context.Collections
                .Where(c => c.UserId == userId)
                .Where(c => c.Article.Category_Articles
                    .Any(ca => ca.Category.Is_principal && !string.IsNullOrEmpty(ca.Category.Name)))
                // Ajoutez la condition pour searchString2 ici
                .Where(c => string.IsNullOrEmpty(searchString2) || c.Article.Category_Articles
                    .Any(ca => ca.Category.Is_principal && ca.Category.Name.Contains(searchString2)))
                .GroupBy(c => c.Article.Category_Articles
                    .FirstOrDefault(ca => ca.Category.Is_principal).Category.Name)
                .Select(g => new CategoryExpense
                {
                    CategoryName = g.Key,
                    TotalSpent = g.Sum(c => (decimal?)c.Purchased_Price) ?? 0
                })
                .ToListAsync();

            var monthlyExpenses = await _context.Collections
                .Where(c => c.UserId == userId && c.Purchased_Date.HasValue && c.Purchased_Date.Value.Year == SelectedYear)
                .GroupBy(c => c.Purchased_Date.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalSpent = g.Sum(c => (decimal?)c.Purchased_Price) ?? 0
                })
                .ToListAsync();

            MonthlyExpenses = Enumerable.Range(1, 12)
                .Select(m => monthlyExpenses.FirstOrDefault(e => e.Month == m)?.TotalSpent ?? 0)
                .ToList();

            if (SortColumn == "CategoryName")
            {
                CategoryExpensesFiltered = SortOrder == "asc" ?
                    CategoryExpensesFiltered.OrderBy(c => c.CategoryName).ToList() :
                    CategoryExpensesFiltered.OrderByDescending(c => c.CategoryName).ToList();
            }
            else if (SortColumn == "TotalSpent")
            {
                CategoryExpensesFiltered = SortOrder == "asc" ?
                    CategoryExpensesFiltered.OrderBy(c => c.TotalSpent).ToList() :
                    CategoryExpensesFiltered.OrderByDescending(c => c.TotalSpent).ToList();
            }

            if (SortColumn2 == "CategoryName")
            {
                CategoryExpenses = SortOrder2 == "asc" ?
                    CategoryExpenses.OrderBy(c => c.CategoryName).ToList() :
                    CategoryExpenses.OrderByDescending(c => c.CategoryName).ToList();
            }
            else if (SortColumn2 == "TotalSpent")
            {
                CategoryExpenses = SortOrder2 == "asc" ?
                    CategoryExpenses.OrderBy(c => c.TotalSpent).ToList() :
                    CategoryExpenses.OrderByDescending(c => c.TotalSpent).ToList();
            }
        }
    }
}
