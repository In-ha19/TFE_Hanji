// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Gestionnaire_Collections.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        //public InputModel User { get; set; }
        public UserViewModelCreate User { get; set; } = new UserViewModelCreate();
        public string ConfirmationMessage { get; set; }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /*public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }*/


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Vérifier si un utilisateur avec cet email existe déjà
                var existingEmailUser = await _userManager.FindByEmailAsync(User.Email);
                if (existingEmailUser != null)
                {
                    ModelState.AddModelError("User.Email", "Cet email est déjà utilisé.");
                    return Page();
                }

                // Vérifier si un utilisateur avec ce login existe déjà
                var existingLoginUser = await _userManager.FindByNameAsync(User.Login);
                if (existingLoginUser != null)
                {
                    ModelState.AddModelError("User.Login", "Ce login est déjà utilisé.");
                    return Page();
                }

                // Créer un nouvel utilisateur
                var user = new ApplicationUser
                {
                    UserName = User.Login,
                    Email = User.Email,
                    IsAdmin = User.IsAdmin,
                    IsActive = true,
                    EmailConfirmed = false 
                };
                try
                {
                    var result = await _userManager.CreateAsync(user, User.Password);

                    if (result.Succeeded)
                    {   
                        await _userManager.AddToRoleAsync(user, "User");

                        // Générer le token de confirmation d'email
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        // Envoyer l'email de confirmation
                        await _emailSender.SendEmailAsync(User.Email, "Confirmez votre email",
                            $"Veuillez confirmer votre compte en <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquant ici</a>.");

                        ConfirmationMessage = "Un email de confirmation a été envoyé. Veuillez vérifier votre boîte de réception.";

                        //return RedirectToPage("/Index"); 
                        return Page();
                        
                    }

                    foreach (var error in result.Errors)
                    {
                        switch (error.Code)
                        {
                            case "DuplicateUserName":
                                ModelState.AddModelError("User.Login", "Ce login est déjà utilisé.");
                                break;
                            case "DuplicateEmail":
                                ModelState.AddModelError("User.Email", "Cet email est déjà utilisé.");
                                break;
                            case "PasswordTooShort":
                                ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins 6 caractères.");
                                break;
                            case "PasswordRequiresNonAlphanumeric":
                                ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins un caractère spécial.");
                                break;
                            case "PasswordRequiresUpper":
                                ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins une lettre majuscule.");
                                break;
                            case "PasswordRequiresLower":
                                ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins une lettre minuscule.");
                                break;
                            case "PasswordRequiresDigit":
                                ModelState.AddModelError("User.Password", "Le mot de passe doit contenir au moins un chiffre.");
                                break;
                            case "InvalidUserName":
                                ModelState.AddModelError("User.Login", "Le nom d'utilisateur ne peut contenir que des lettres et des chiffres. Aucun accent n'est accepté");
                                break;
                            case "UserLockedOut":
                                ModelState.AddModelError("User.LockoutEnabled", "L'utilisateur est actuellement bloqué.");
                                break;
                            case "PasswordTooLong":
                                ModelState.AddModelError("User.Password", "Le mot de passe est trop long. Il ne peut pas dépasser 128 caractères.");
                                break;
                            case "UserNameTooLong":
                                ModelState.AddModelError("User.Login", "Le nom d'utilisateur est trop long. Il ne peut pas dépasser 256 caractères.");
                                break;
                            default:
                                ModelState.AddModelError(string.Empty, error.Description);
                                break;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Capture l'exception SQL liée à la clé dupliquée
                    if (ex.Number == 2601 || ex.Number == 2627) // Numéro d'erreur pour clé dupliquée
                    {
                        ModelState.AddModelError(string.Empty, "L'email ou le login existe déjà dans la base de données.");
                    }
                    else
                    {
                        // Autre exception SQL
                        ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création de l'utilisateur.");
                    }
              
#if DEBUG
                    var bg = Console.BackgroundColor;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("Erreur lors de l'envoi de mail :");
                    Console.Write(ex.Message);
                    Console.BackgroundColor = bg;
                    Console.WriteLine("");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("Hint : Est-ce que paper cut est allumé?");
                    Console.BackgroundColor = bg;
                    Console.WriteLine("");
#endif
                
                }                            
            }

            return Page();
        }
       /* private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }*/

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
