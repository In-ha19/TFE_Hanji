using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Collection
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString(); // Utilisateur propriétaire de la collection
        public ApplicationUser User { get; set; }

        public string ArticleId { get; set; } // Identifiant de l'article 
        public Article Article { get; set; }
        [Display(Name = "Evaluation")]
        public float? Note { get; set; } // Note donnée à l’article
        [Display(Name = "Satut")]
        public int Statut { get; set; } // Statut (ex: 0 = à lire, 1 = en cours, 2 = terminé, 3 = Aucun) 
        [Display(Name = "Note")]
        public string? Text { get; set; } // Commentaire de l’utilisateur
        [Display(Name = "Prix d'achat")]
        public float? Purchased_Price { get; set; } // Prix d’achat
        [Display(Name = "Date d'achat")]
        public DateTime? Purchased_Date{ get; set; } // Date d’achat

    }
}
