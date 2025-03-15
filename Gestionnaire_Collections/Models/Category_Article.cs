namespace Gestionnaire_Collections.Models
{
    public class Category_Article
    {
        public string CategoryId { get; set; } // Identifiant de la catégorie
        public Category Category { get; set; }

        public string ArticleId { get; set; } // Identifiant de l’article
        public Article Article { get; set; }
    }
}
