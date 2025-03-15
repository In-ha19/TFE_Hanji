using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Display(Name = "Objet du message")]
        public string MessageObjet { get; set; } = string.Empty;
        [Display(Name = "Message")]
        public string Text { get; set; } = string.Empty;
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Statut")]
        public int Statut { get; set; }
        [Display(Name = "User")]
        public string UserId { get; set; } 
        public ApplicationUser User { get; set; }
    }
}
