using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Models;
using Gestionnaire_Collections.Data;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Messages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Message == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
