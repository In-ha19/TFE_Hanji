using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Shared.Families
{
    [Authorize]
    public class IndexMyFamiliesModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexMyFamiliesModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public PaginatedList<Family> Family { get; set; } = null!;
        public List<string> RequestsFamilyIds { get; set; } = new List<string>();

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";
        public string SearchQuery { get; set; }

        public async Task OnGetAsync(string searchString, int? statusFilter)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            const int pageSize = 10;

            var query = _context.Family_Users
                .Where(fu => fu.UserId == userId && fu.Is_manager)
                .Include(fu => fu.Family)
                .Select(fu => fu.Family)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                string normalizedSearchString = searchString.ToLower();
                query = query.Where(a =>
                    a.Name.ToLower().Contains(normalizedSearchString) ||
                    a.Description.ToLower().Contains(normalizedSearchString));
            }

            query = SortColumn switch
            {
                "Name" => SortOrder == "asc" ? query.OrderBy(a => a.Name) : query.OrderByDescending(a => a.Name),
                "Description" => SortOrder == "asc" ? query.OrderBy(a => a.Description) : query.OrderByDescending(a => a.Description),
                _ => query.OrderBy(a => a.Name),
            };

            if (statusFilter.HasValue)
            {
                var familyIdsWithStatus = await _context.MemberShip_Families
                    .Where(mf => mf.Statut == statusFilter && query.Select(f => f.Id).Contains(mf.FamilyId))
                    .Select(mf => mf.FamilyId)
                    .Distinct()
                    .ToListAsync();

                query = query.Where(f => familyIdsWithStatus.Contains(f.Id));
            }

            Family = await PaginatedList<Family>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);

            RequestsFamilyIds = await _context.MemberShip_Families
                .Where(mf => mf.Statut == 0 &&
                             Family.Select(f => f.Id).Contains(mf.FamilyId))
                .Select(mf => mf.FamilyId)
                .Distinct()
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Families.FindAsync(id);
            if (family != null)
            {
                var familyUsers = await _context.Family_Users.Where(fu => fu.FamilyId == id).ToListAsync();
                _context.Family_Users.RemoveRange(familyUsers);

                var membershipFamilies = await _context.MemberShip_Families.Where(mf => mf.FamilyId == id).ToListAsync();
                _context.MemberShip_Families.RemoveRange(membershipFamilies);

                _context.Families.Remove(family);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
