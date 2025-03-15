using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.DTO.Shared
{
    public class CategoryDTO
    {
        [Display(Name = "Nom")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Active")]
        public bool Is_active { get; set; }
        [Display(Name = "Principal")]
        public bool Is_principal { get; set; }
    }
}
