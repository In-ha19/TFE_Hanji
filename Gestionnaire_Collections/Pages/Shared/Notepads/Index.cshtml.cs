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

namespace Gestionnaire_Collections.Pages.Shared.Notepads
{
    [IgnoreAntiforgeryToken]

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

        public PaginatedList<Notepad> Notes { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";
        public string SearchQuery { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task OnGetAsync(string searchString, string dateFilter)
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
                var query = _context.Notepads
                    .Where(m => m.UserId == currentUserId)
                    .OrderByDescending(m => m.Date_reminder)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(searchString))
                {
                    string normalizedSearchString = searchString.ToLower();
                    query = query.Where(a =>
                        a.Titre.ToLower().Contains(normalizedSearchString));
                }

                if (!string.IsNullOrEmpty(dateFilter))
                {
                    var date = DateTime.Parse(dateFilter);
                    query = query.Where(m => m.Date_reminder == date.Date);
                }

                query = SortColumn switch
                {
                    "Titre" => SortOrder == "asc" ? query.OrderBy(a => a.Titre) : query.OrderByDescending(a => a.Titre),
                    "Date_reminder" => SortOrder == "asc" ? query.OrderBy(a => a.Date_reminder) : query.OrderByDescending(a => a.Date_reminder),
                    _ => query.OrderBy(a => a.Titre),
                };

                Notes = await PaginatedList<Notepad>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Une erreur est survenue lors de la récupération des notes.";
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("ID de la note est manquant.");
            }

            var note = await _context.Notepads.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            _context.Notepads.Remove(note);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "La note a été supprimée avec succès.";
            return RedirectToPage("./Index");
        }
        public async Task<JsonResult> OnGetNoteDetailsAsync(int id)
        {
            var note = await _context.Notepads.FindAsync(id);

            if (note == null)
            {
                return new JsonResult(new { success = false, message = "Note introuvable." });
            }

            return new JsonResult(new
            {
                success = true,
                data = new
                {
                    note.Id,
                    note.Titre,
                    note.Text,
                    Date_reminder = note.Date_reminder?.ToString("dd-MM-yyyy") 
                }
            });
        }
        public async Task<JsonResult> OnPostUpdateNoteAsync(int id, string titre, string text, DateTime? dateReminder)
        {
            if (id == 0)
            {
                return new JsonResult(new { success = false, message = "ID de la note est manquant." });
            }

            var note = await _context.Notepads.FindAsync(id);

            if (note == null)
            {
                return new JsonResult(new { success = false, message = "Note introuvable." });
            }

            note.Titre = titre;
            note.Text = text;
            note.Date_reminder = dateReminder;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "La note a été mise à jour avec succès." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JsonResult(new { success = false, message = "Erreur lors de la mise à jour de la note." });
            }
        }
    }
}
