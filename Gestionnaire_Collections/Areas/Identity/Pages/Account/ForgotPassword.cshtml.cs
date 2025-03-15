using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

public class ForgotPasswordModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public bool MessageSent { get; set; } = false;

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user == null)
        {
            MessageSent = true;
            return Page();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var callbackUrl = Url.Page(
            "/Account/ResetPassword",
            pageHandler: null,
            values: new { userId = user.Id, code },
            protocol: Request.Scheme);

        try
        {
            await _emailSender.SendEmailAsync(
                Input.Email,
                "Réinitialisation de votre mot de passe",
                $"Bonjour {user.UserName},<br><br>Veuillez réinitialiser votre mot de passe en <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquant ici</a>.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'envoi de l'email : {ex.Message}");

            ModelState.AddModelError("", "Le mail de réinitialisation n'a pas pu être envoyé. Vérifiez que le serveur mail est bien actif.");
        }

        MessageSent = true;
        return Page();
    }

}
