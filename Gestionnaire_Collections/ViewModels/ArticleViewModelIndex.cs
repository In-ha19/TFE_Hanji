using Gestionnaire_Collections.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class ArticleViewModelIndex
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Edition")]
        public string? Edition { get; set; }
        [Display(Name = "Auteur")]
        public string? Autor_name { get; set; }
        [Display(Name = "Date de parution")]       
        public string? Date { get; set; }
        [Display(Name = "Actif")]
        public bool Is_active { get; set; }

        public List<Category_Article> Category_Articles { get; set; } = new List<Category_Article>();
    }
}
