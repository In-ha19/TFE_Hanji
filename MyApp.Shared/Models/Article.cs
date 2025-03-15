using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Article
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
        public List<Category_Article> Category_Articles { get; set; } = new List<Category_Article>();
        public List<Collection> Collections { get; set; } = new List<Collection>();      
    }
}
