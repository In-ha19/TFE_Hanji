using Gestionnaire_Collections.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class CategoryViewModelCreate
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = "Le Nom est obligatoire.")]
        [Display(Name = "Nom")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Principal")]
        public bool Is_principal { get; set; }
        public string? Parent_id { get; set; }
       
    }
}
