namespace Gestionnaire_Collections.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty; //Type d'image (png, jpg, etc)
        public string Root { get; set; } = string.Empty; // Chemin de stockage de l’image

        public string ArticleId { get; set; } //Identifiant de l'article
        public Article Article { get; set; }
    }
}
