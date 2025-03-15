using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        [Display(Name = "Login")]
        public string Login { get; set; } = string.Empty;
        [Display(Name = "Mot de Passe")]
        public string Pswd { get; set; } = string.Empty;
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Admin")]
        public bool Is_admin { get; set; }
        [Display(Name = "Active")]
        public bool Is_active { get; set; }

        public List<Family_User> Family_Users { get; set; } = new List<Family_User>();
        public List<Collection> Collections { get; set; } = new List<Collection>();
        public List<Notepad> Notepads { get; set; } = new List<Notepad>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
