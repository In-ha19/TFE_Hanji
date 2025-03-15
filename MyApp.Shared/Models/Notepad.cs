using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Notepad
    {   
        public int Id { get; set; }
        [Display(Name = "Titre")]
        public string Titre { get; set; } = string.Empty;
        [Display(Name = "Note")]
        public string? Text { get; set; }
        [Display(Name = "Date de rappel")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "Non spécifié")]
        public DateTime? Date_reminder { get; set; }
        public bool IsEmailSent { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
