using Gestionnaire_Collections.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text;
using System.Text.Encodings.Web;
using Gestionnaire_Collections.DTO.User;
using Microsoft.EntityFrameworkCore;
using Gestionnaire_Collections.Data;
using Microsoft.AspNetCore.Authorization;

namespace Gestionnaire_Collections.Pages.User.Profil
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
        public UserViewModelEdit UserEdit { get; set; } = new();
        public int NumberOfFamiliesAsManager { get; set; }
        public int NumberOfFamiliesNotManager { get; set; }
        public int NumberOfMessagesSent { get; set; }
        public List<CategoryArticleCountDTO> ArticleCountByCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Le nom de l'utilisateur est requis.");
            }

            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            UserEdit = new UserViewModelEdit
            {
                Login = user.UserName,
                Email = user.Email,             
            };

            var userId = _userManager.GetUserId(User);

            NumberOfFamiliesAsManager = _context.Family_Users
                .Where(fu => fu.UserId == userId && fu.Is_manager)
                .Count();

            NumberOfFamiliesNotManager = _context.Family_Users
                .Where(fu => fu.UserId == userId && !fu.Is_manager)
                .Count();

            NumberOfMessagesSent = _context.Messages
                .Where(m => m.UserId == userId)
                .Count();

            ArticleCountByCategory = _context.Collections
            .Where(c => c.UserId == userId)
            .Join(_context.Articles,
                collection => collection.ArticleId,
                article => article.Id,
                (collection, article) => new { collection, article })
            .SelectMany(a => a.article.Category_Articles)
            .Join(_context.Categories,
                categoryArticle => categoryArticle.CategoryId,
                category => category.Id,
                (categoryArticle, category) => new { categoryArticle, category })
            .Where(c => c.category.Is_principal && c.category.Is_active)  
            .GroupBy(x => x.category.Name)  
            .Select(g => new CategoryArticleCountDTO
            {
                CategoryName = g.Key,
                ArticleCount = g.Count()
            })
            .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string login)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage(new { name = login });

            }

            var user = await _userManager.FindByNameAsync(login);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            var existingUserWithLogin = await _userManager.FindByNameAsync(UserEdit.Login);
            if (existingUserWithLogin != null && existingUserWithLogin.Id != user.Id)
            {
                TempData["ErrorMessage"] = "Ce login est déjà utilisé par un autre utilisateur.";
                return RedirectToPage(new { name = login });

            }

            var existingEmailUser = await _userManager.FindByEmailAsync(UserEdit.Email);
            if (existingEmailUser != null && existingEmailUser.Id != user.Id)
            {
                TempData["ErrorMessage"] = "Cet email est déjà utilisé par un autre utilisateur.";
                return RedirectToPage(new { name = login });
            }

            user.UserName = UserEdit.Login;
            user.Email = UserEdit.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    //ModelState.AddModelError(string.Empty, error.Description);
                    List<string> errorMessages = result.Errors.Select(error => error.Description).ToList();

                    TempData["ErrorMessages"] = string.Join("|", errorMessages);
                }
                return RedirectToPage(new { name = login });
            }
            TempData["SuccessMessage"] = "Profil mis à jour avec succès.";

            await _signInManager.RefreshSignInAsync(user);

            return RedirectToPage(new { name = UserEdit.Login });
        }

        public async Task<IActionResult> OnPostSendResetLinkAsync(string login)
        {
                var user = await _userManager.FindByNameAsync(login);
                if (user == null)
                {
                    return NotFound("Utilisateur non trouvé.");
                }

                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName))
                {
                    ModelState.AddModelError("", "Les informations de l'utilisateur sont incomplètes.");
                    return RedirectToPage(new { name = login });
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code },
                    protocol: Request.Scheme);

                if (string.IsNullOrEmpty(callbackUrl))
                {
                    ModelState.AddModelError("", "L'URL de réinitialisation est invalide.");
                    return RedirectToPage(new { name = login });
                }

                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Réinitialisation de votre mot de passe",
                    $"Bonjour {user.UserName},<br><br>Veuillez réinitialiser votre mot de passe en <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquant ici</a>."
                );

                TempData["Message"] = "Un lien de réinitialisation vous a été envoyé.";
                return RedirectToPage(new { name = login });
        }
    }
}
