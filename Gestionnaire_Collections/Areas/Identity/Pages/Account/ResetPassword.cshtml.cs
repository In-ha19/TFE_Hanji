using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

namespace Gestionnaire_Collections.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ResetPasswordModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string UserId { get; set; }

        [BindProperty]
        public string Code { get; set; }

        public void OnGet(string userId, string code)
        {
            UserId = userId;           
            Code = code;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TranslateModelErrors();
                return Page(); 
            }

                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null) return RedirectToPage("ResetPasswordConfirmation");

                var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));

                var result = await _userManager.ResetPasswordAsync(user, decodedCode, Password);     
                if (!result.Succeeded)
                {
                    // Gestion des erreurs
                    foreach (var error in result.Errors)
                    {
                        switch (error.Code)
                        {
                            case "PasswordTooShort":
                                ModelState.AddModelError(string.Empty, "Le mot de passe doit contenir au moins 6 caractères.");
                                break;
                            case "PasswordRequiresNonAlphanumeric":
                                ModelState.AddModelError(string.Empty, "Le mot de passe doit contenir au moins un caractère spécial.");
                                break;
                            case "PasswordRequiresUpper":
                                ModelState.AddModelError(string.Empty, "Le mot de passe doit contenir au moins une lettre majuscule.");
                                break;
                            case "PasswordRequiresLower":
                                ModelState.AddModelError(string.Empty, "Le mot de passe doit contenir au moins une lettre minuscule.");
                                break;
                            case "PasswordRequiresDigit":
                                ModelState.AddModelError(string.Empty, "Le mot de passe doit contenir au moins un chiffre.");
                                break;
                            case "InvalidToken":
                                ModelState.AddModelError(string.Empty, "Le lien de réinitialisation du mot de passe est invalide. Veuillez demander un nouveau lien.");
                                break;
                            default:
                                ModelState.AddModelError(string.Empty, error.Description);
                                break;
                        }
                    }
                    return Page();
                }

                await _signInManager.SignOutAsync();
                Response.Cookies.Delete(".AspNetCore.Identity.Application");
                return RedirectToPage("ResetPasswordConfirmation");         
        }
        private void TranslateModelErrors()
        {
            // On récupère une copie des erreurs existantes
            var originalErrors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
            );

            // On efface les erreurs existantes pour les remplacer par les versions traduites
            ModelState.Clear();

            foreach (var kvp in originalErrors)
            {
                foreach (var error in kvp.Value)
                {
                    var translatedMessage = error switch
                    {
                        "The Password field is required." => "Le mot de passe est requis.",
                        "The Password must be at least 6 characters long." => "Le mot de passe doit contenir au moins 6 caractères.",
                        "Passwords must have at least one non alphanumeric character." => "Le mot de passe doit contenir au moins un caractère spécial.",
                        "Passwords must have at least one uppercase ('A'-'Z')." => "Le mot de passe doit contenir au moins une lettre majuscule.",
                        "Passwords must have at least one lowercase ('a'-'z')." => "Le mot de passe doit contenir au moins une lettre minuscule.",
                        "Passwords must have at least one digit ('0'-'9')." => "Le mot de passe doit contenir au moins un chiffre.",
                        _ => error // Si aucune traduction n'est trouvée, conserver l'original
                    };

                    ModelState.AddModelError(kvp.Key, translatedMessage);
                }
            }
        }

    }
}
