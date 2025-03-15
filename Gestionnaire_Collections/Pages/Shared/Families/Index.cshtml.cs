using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Shared.Families
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

        public PaginatedList<Family> Family { get; set; } = null!;
        public ApplicationUser CurrentUser { get; set; }
        //Liste pour obtenir les IdFamilies de l'utilisateur connecté
        public List<string> UserFamilyIds { get; set; } = new List<string>();
        public bool IsUserManagerInAnyFamily { get; set; }
        public bool IsUserInAnyFamily { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";
        public string SearchQuery { get; set; }
        public Dictionary<string, int> FamilyRequestStatuses { get; set; } = new Dictionary<string, int>();

        public async Task OnGetAsync(string searchString, int? statusFilter)
        {
            const int pageSize = 10;
            
            var query = _context.Families.AsQueryable();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CurrentUser = await _userManager.FindByIdAsync(userId);

            UserFamilyIds = await _context.Family_Users
                .Where(fu => fu.UserId == userId)
                .Select(fu => fu.FamilyId)
                .ToListAsync();

            IsUserManagerInAnyFamily = await _context.Family_Users
                .AnyAsync(fu => fu.UserId == userId && fu.Is_manager);

            IsUserInAnyFamily = await _context.Family_Users
                .AnyAsync(fu => fu.UserId == userId);

            FamilyRequestStatuses = await _context.MemberShip_Families
                .Where(mf => mf.RequesterId == userId)
                .ToDictionaryAsync(mf => mf.FamilyId, mf => mf.Statut);

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
                if (statusFilter.Value == 0) 
                {
                    query = query.Where(f => _context.MemberShip_Families
                        .Any(mf => mf.FamilyId == f.Id && mf.RequesterId == userId && mf.Statut == 0));
                }
                else if (statusFilter.Value == 1) 
                {
                    query = query.Where(f => UserFamilyIds.Contains(f.Id));
                }
                else if (statusFilter.Value == 2)
                {
                    query = query.Where(f =>
                        !_context.MemberShip_Families.Any(mf => mf.FamilyId == f.Id && mf.RequesterId == userId) &&
                        !_context.Family_Users.Any(fu => fu.FamilyId == f.Id && fu.UserId == userId )
                    );
                }
            }

            Family = await PaginatedList<Family>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);
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

        public async Task<IActionResult> OnPostRequestMembershipAsync(string familyId, string searchString, int? statusFilter)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Vérifiez si une demande existe déjà
            var existingRequest = await _context.MemberShip_Families
                .FirstOrDefaultAsync(mf => mf.FamilyId == familyId && mf.RequesterId == userId);

            if (existingRequest == null)
            {
                var managerId = await _context.Family_Users
                    .Where(fu => fu.FamilyId == familyId && fu.Is_manager)
                    .Select(fu => fu.UserId)
                    .FirstOrDefaultAsync();

                var newRequest = new MemberShip_Family
                {
                    FamilyId = familyId,
                    RequesterId = userId,
                    Statut = 0 // 0 = En attente
                };

                _context.MemberShip_Families.Add(newRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { PageIndex = PageIndex, searchString = searchString, statusFilter = statusFilter });
        }
    }
}
