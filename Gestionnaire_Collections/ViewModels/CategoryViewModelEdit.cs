using Gestionnaire_Collections.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class CategoryViewModelEdit
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le Nom est obligatoire.")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Active")]
        public bool Is_active { get; set; }
        [Display(Name = "Principal")]
        public bool Is_principal { get; set; }
        [Display(Name = "Catégorie parent")]
        public string? Parent_id { get; set; }

    }
}
