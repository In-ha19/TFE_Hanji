using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class UserViewModelCreate
    {
        public bool IsAdmin { get; set; }
        [Required(ErrorMessage = "Le login est requis.")]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

    }
}
