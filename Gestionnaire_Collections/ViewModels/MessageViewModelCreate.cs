using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class MessageViewModelCreate
    {
        public int Id { get; set; }
        [Display(Name = "Objet du message")]
        [Required(ErrorMessage = "L'Object du message est obligatoire.")]
        public string MessageObjet { get; set; } = string.Empty;
        [Display(Name = "Message")]
        [Required(ErrorMessage = "Le Message est obligatoire.")]
        public string Text { get; set; } = string.Empty;
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Statut")]
        public int Statut { get; set; }
        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}
