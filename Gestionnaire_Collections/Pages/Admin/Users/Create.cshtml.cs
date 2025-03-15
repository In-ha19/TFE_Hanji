using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Gestionnaire_Collections.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public UserViewModelCreate User { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var existingEmailUser = await _userManager.FindByEmailAsync(User.Email);
                if (existingEmailUser != null)
                {
                    ModelState.AddModelError("User.Email", "Cet email est déjà utilisé.");
                    return Page();
                }

                var existingLoginUser = await _userManager.FindByNameAsync(User.Login);
                if (existingLoginUser != null)
                {
                    ModelState.AddModelError("User.Login", "Ce login est déjà utilisé.");
                    return Page();
                }
              
                var user = new ApplicationUser
                {
                    UserName = User.Login,
                    Email = User.Email,
                    IsAdmin = User.IsAdmin,
                    IsActive = true,
                    EmailConfirmed = true 
                };
                try
                {
                    var result = await _userManager.CreateAsync(user, User.Password);

                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        }

                        if (!await _roleManager.RoleExistsAsync("User"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("User"));
                        }

                        if (User.IsAdmin)
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                        }

                        return RedirectToPage("/Index");
                    }

                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "DuplicateUserName")
                        {
                            ModelState.AddModelError("User.Login", "Ce login est déjà utilisé.");
                        }
                        else if (error.Code == "DuplicateEmail")
                        {
                            ModelState.AddModelError("User.Email", "Cet email est déjà utilisé.");
                        }
                        else if (error.Code == "PasswordTooShort")
                        {
                            ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins 6 caractères.");
                        }
                        else if (error.Code == "PasswordRequiresNonAlphanumeric")
                        {
                            ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins un caractère spécial.");
                        }
                        else if (error.Code == "PasswordRequiresUpper")
                        {
                            ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins une lettre majuscule.");
                        }
                        else if (error.Code == "PasswordRequiresLower")
                        {
                            ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins une lettre minuscule.");
                        }
                        else if (error.Code == "PasswordRequiresDigit")
                        {
                            ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins un chiffre.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2601 || ex.Number == 2627) 
                    {
                        ModelState.AddModelError(string.Empty, "L'email ou le login existe déjà dans la base de données.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création de l'utilisateur.");
                    }
                }

            }
                return Page();
        }
    }
}
