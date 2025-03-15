using Gestionnaire_Collections.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class CollectionViewModelDetailsMyCollection
    {
       // public string ArticleId { get; set; }
        [Display(Name = "Evaluation")]
        public float? Note { get; set; }
        [Display(Name = "Satut")]
        public int Statut { get; set; }
        [Display(Name = "Note")]
        public string? Text { get; set; }
        [Display(Name = "Prix d'achat")]
        public float? Purchased_Price { get; set; }
        [Display(Name = "Date d'achat")]
        public DateTime? Purchased_Date { get; set; }
    }
}
