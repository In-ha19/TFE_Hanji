using Gestionnaire_Collections.Models;
using System.ComponentModel.DataAnnotations;
using Gestionnaire_Collections.DTO.Admin.Articles;

namespace Gestionnaire_Collections.ViewModels
{
    public class ArticleViewModelDetails
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string? Edition { get; set; }
        public string? Isbn { get; set; }
        public string? Autor_name { get; set; }
        public string? Date { get; set; }
        public string? Summary { get; set; }
        public bool Is_active { get; set; }
        public float? Price { get; set; }

        [Display(Name = "Image")]
        public string? ImageRoot { get; set; }
        public string SelectedCategoryName { get; set; } = string.Empty;
        public List<string>? SelectedCategoryNameSecondary { get; set; } = new List<string>();
        public List<CollectionDTO> Collections { get; set; } = new List<CollectionDTO>();
    }
}
