using System;
using System.Threading.Tasks;
using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestionnaire_Collections.Pages.Shared.Notepads
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public NotepadViewModelCreate Note { get; set; } = new NotepadViewModelCreate();
        public string UserId { get; set; }

        public IActionResult OnGet()
        {
            var currentUserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToPage("./Index");
            }

            UserId = currentUserId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var notepad = new Notepad
            {
                Titre = Note.Titre,
                Text = Note.Text,
                Date_reminder = Note.Date_reminder,
                UserId = _userManager.GetUserId(User)
            };

            _context.Notepads.Add(notepad);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
