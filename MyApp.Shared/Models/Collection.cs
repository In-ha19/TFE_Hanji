using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Collection
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public ApplicationUser User { get; set; }

        public string ArticleId { get; set; }
        public Article Article { get; set; }
        [Display(Name = "Evaluation")]
        public float? Note { get; set; }
        [Display(Name = "Satut")]
        public int Statut { get; set; }
        [Display(Name = "Note")]
        public string? Text { get; set; }
        [Display(Name = "Prix d'achat")]
        public float? Purchased_Price { get; set; }
        [Display(Name = "Date d'achat")]
        public DateTime? Purchased_Date{ get; set; }

    }
}
