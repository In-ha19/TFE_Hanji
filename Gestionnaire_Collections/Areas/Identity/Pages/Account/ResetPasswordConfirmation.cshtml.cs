using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestionnaire_Collections.Areas.Identity.Pages.Account
{
    public class ResetPasswordConfirmationModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string Status { get; set; }

        public void OnGet()
        {
            // Si tu veux passer un message de statut, tu peux le faire ici
            Status = "Votre mot de passe a été réinitialisé avec succès.";
        }
    }
}
