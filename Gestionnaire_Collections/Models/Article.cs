using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Article
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } // Nom de l’article
        public string? Edition { get; set; } // Édition de l’article
        public string? Isbn { get; set; } // Code ISBN si applicable
        public string? Autor_name { get; set; } // Nom de l’auteur
        public string? Date { get; set; } // Date de publication
        public string? Summary { get; set; }  // Résumé de l’article
        public bool Is_active { get; set; } // Indique si l’article est actif
        public float? Price { get; set; } // Prix de l’article
        public List<Category_Article> Category_Articles { get; set; } = new List<Category_Article>();
        public List<Collection> Collections { get; set; } = new List<Collection>();      
    }
}
