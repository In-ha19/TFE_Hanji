using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Notepad
    {   
        public int Id { get; set; }
        [Display(Name = "Titre")]
        public string Titre { get; set; } = string.Empty; // Titre de la note
        [Display(Name = "Note")]
        public string? Text { get; set; } // Contenu de la note
        [Display(Name = "Date de rappel")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "Non spécifié")]
        public DateTime? Date_reminder { get; set; } // Date de rappel
        public bool IsEmailSent { get; set; } // Indique si un rappel a été envoyé
        public string UserId { get; set; } // Identifiant de l’utilisateur
        public ApplicationUser User { get; set; }
    }
}
