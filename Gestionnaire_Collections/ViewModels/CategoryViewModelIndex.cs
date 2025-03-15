using Gestionnaire_Collections.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class CategoryViewModelIndex
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Display(Name = "Nom")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Active")]
        public bool Is_active { get; set; }
        [Display(Name = "Principal")]
        public bool Is_principal { get; set; }
        [Display(Name = "Nom de la catégorie principale")]
        public string? Parent_id { get; set; }
        [Display(Name = "Nom de la catégorie principale")]
        public Category Parent { get; set; }

        [Display(Name = "Nom de la catégorie principale")]
        public string? Parent_name { get; set; }
    }
}
