using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.Models
{
    public class Family_User
    {
        public string FamilyId { get; set; } // Identifiant de la famille
        public Family Family { get; set; }

        public string UserId { get; set; } // Identifiant de l’utilisateur
        public ApplicationUser User { get; set; }

        [Display(Name = "Manager")]
        public bool Is_manager { get; set; } // Indique si l’utilisateur est manager

    }
}
