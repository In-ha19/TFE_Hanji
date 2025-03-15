using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Family
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Display(Name = "Nom de la famille")]
        [Required(ErrorMessage = "Le Nom de la famille est obligatoire.")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public List<Family_User> Family_Users { get; set; } = new List<Family_User>();
    }
}
