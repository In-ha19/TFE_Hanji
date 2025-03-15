using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class NotepadViewModelCreate
    {
        public int Id { get; set; }
        [Display(Name = "Titre")]
        [Required(ErrorMessage = "Le titre est obligatoire.")]
        public string Titre { get; set; } = string.Empty;
        [Display(Name = "Note")]
        public string? Text { get; set; }
        [Display(Name = "Date de rappel")]
        public DateTime? Date_reminder { get; set; }

        public string UserId { get; set; }
    }
}
