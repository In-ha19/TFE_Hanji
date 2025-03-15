using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class UserViewModelIndex
    {
        
        [Display(Name = "Nom utilisateur")]
        [Required(ErrorMessage = "Le login est requis.")]
        public string Login { get; set; } = string.Empty;
        [Display(Name = "Adresse email")]
        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Email Confirmé")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Actif")]
        public bool IsActive { get; set; }

    }
}
