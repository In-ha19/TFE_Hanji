using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire_Collections.Pages.Messages
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

        public PaginatedList<Message> Messages { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";
        public string SearchQuery { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task OnGetAsync(string searchString, string dateFilter, int? statusFilter)
        {
            const int pageSize = 10;

            var currentUserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(currentUserId))
            {
                ErrorMessage = "Utilisateur non identifié.";
                return;
            }

            try
            {
                var query = _context.Messages
                    .Where(m => m.UserId == currentUserId)
                    .OrderByDescending(m => m.Date)
                     .AsQueryable();

                if (!string.IsNullOrEmpty(searchString))
                {
                    string normalizedSearchString = searchString.ToLower();
                    query = query.Where(a =>
                        a.MessageObjet.ToLower().Contains(normalizedSearchString));
                }

                if (!string.IsNullOrEmpty(dateFilter))
                {
                    var date = DateTime.Parse(dateFilter);
                    query = query.Where(m => m.Date.Date == date.Date);
                }

                if (statusFilter.HasValue)
                {
                    query = query.Where(m => m.Statut == statusFilter.Value);
                }

                query = SortColumn switch
                {
                    "MessageObjet" => SortOrder == "asc" ? query.OrderBy(a => a.MessageObjet) : query.OrderByDescending(a => a.MessageObjet),
                    "Date" => SortOrder == "asc" ? query.OrderBy(a => a.Date) : query.OrderByDescending(a => a.Date),
                    _ => query.OrderBy(a => a.MessageObjet),
                };

                Messages = await PaginatedList<Message>.CreateAsync(query, PageIndex, pageSize);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Une erreur est survenue lors de la récupération des messages.";
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("ID du message est manquant.");
            }

            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Le message a été supprimé avec succès.";
            return RedirectToPage("./Index");
        }
    }
}
