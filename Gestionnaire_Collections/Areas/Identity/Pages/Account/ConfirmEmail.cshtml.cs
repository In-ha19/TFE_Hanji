using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

namespace Gestionnaire_Collections.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string Status { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("./Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                ErrorMessage = "L'email a déjà été confirmé.";
                return Page(); 
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));            

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                Status = "Votre email a été confirmé avec succès.";
            }
            else
            {                
                Status = "Une erreur s'est produite lors de la confirmation de l'email. Veuillez essayer à nouveau";
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
