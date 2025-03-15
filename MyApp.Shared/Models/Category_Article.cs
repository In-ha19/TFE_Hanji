namespace Gestionnaire_Collections.Models
{
    public class Category_Article
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
