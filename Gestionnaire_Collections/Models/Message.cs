using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Display(Name = "Objet du message")]
        public string MessageObjet { get; set; } = string.Empty; // Sujet du message
        [Display(Name = "Message")]
        public string Text { get; set; } = string.Empty; // Contenu du message
        [Display(Name = "Date")]
        public DateTime Date { get; set; } // Date d’envoi
        [Display(Name = "Statut")]
        public int Statut { get; set; } // Statut du message (0 = En attente, 1 = Accepté, 2 = Refusé)
        [Display(Name = "User")]
        public string UserId { get; set; } //Identifiant du User ayant envoyé le message
        public ApplicationUser User { get; set; }
    }
}
