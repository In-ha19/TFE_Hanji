using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Display(Name = "Nom")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Active")]
        public bool Is_active { get; set; }
        [Display(Name = "Principal")]
        public bool Is_principal { get; set; }
        public string? Parent_id { get; set; }

        public Category Parent { get; set; } 
        public List<Category> SubCategories { get; set; } = new List<Category>();

        public List<Category_Article> Category_Articles { get; set; } = new List<Category_Article>();
    }
}
