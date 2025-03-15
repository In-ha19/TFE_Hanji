namespace Gestionnaire_Collections.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Root { get; set; } = string.Empty;

        public string ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
