using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using VotreProjet.Services;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public EditModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public UserViewModelEdit UserEdit { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null) return NotFound();

            UserEdit = new UserViewModelEdit
            {
                Login = user.UserName,
                Email = user.Email,
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin"),
                IsActive = user.IsActive,
                EmailConfirmed = user.EmailConfirmed
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string login)
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByNameAsync(login);
            if (user == null) return NotFound();

            // Vérification pour s'assurer que le login est unique
            var existingUserWithLogin = await _userManager.FindByNameAsync(UserEdit.Login);
            if (existingUserWithLogin != null && existingUserWithLogin.Id != user.Id)
            {               
                TempData["ErrorMessage"] = "Ce login est déjà utilisé par un autre utilisateur.";
                //return RedirectToPage(new { name = login });
                UserEdit = new UserViewModelEdit
                {
                    Login = user.UserName,
                    Email = user.Email,
                    IsAdmin = await _userManager.IsInRoleAsync(user, "Admin"),
                    IsActive = user.IsActive,
                    EmailConfirmed = user.EmailConfirmed
                };
                return Page();  
            }

            user.UserName = UserEdit.Login;
            user.EmailConfirmed = UserEdit.EmailConfirmed;
            //user.IsActive = UserEdit.IsActive;
            user.IsAdmin = UserEdit.IsAdmin;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    //ModelState.AddModelError(string.Empty, error.Description);
                    List<string> errorMessages = result.Errors.Select(error => error.Description).ToList();

                    TempData["ErrorMessages"] = string.Join("|", errorMessages);

                    UserEdit = new UserViewModelEdit
                    {
                        Login = user.UserName,
                        Email = user.Email,
                        IsAdmin = await _userManager.IsInRoleAsync(user, "Admin"),
                        IsActive = user.IsActive,
                        EmailConfirmed = user.EmailConfirmed
                    };
                }
               // return RedirectToPage(new { name = login });
                return Page();
            }

            // Gestion des rôles
            if (UserEdit.IsAdmin)
            {
                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                    await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
            }

            return RedirectToPage("/Admin/Users/Index");
        }
        public async Task<IActionResult> OnPostActivateUserAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            user.IsActive = true;
            await _userManager.UpdateAsync(user);

            TempData["SuccessMessage"] = "Utilisateur activé avec succès.";
            return RedirectToPage("./Edit", new { login });
        }

        public async Task<IActionResult> OnPostSendResetLinkAsync(string login)
        {
                var user = await _userManager.FindByNameAsync(login);
                if (user == null)
                {
                    return NotFound();
                }

                // Générer le jeton de réinitialisation de mot de passe
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code },
                    protocol: Request.Scheme);

                // Envoyer l'email de réinitialisation
                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Réinitialisation de votre mot de passe",
                    $"Bonjour {user.UserName},<br><br>Veuillez réinitialiser votre mot de passe en <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquant ici</a>."
                );

                TempData["Message"] = "Un lien de réinitialisation a été envoyé à l'utilisateur.";
                return RedirectToPage(new { login });
        }
    }
}
