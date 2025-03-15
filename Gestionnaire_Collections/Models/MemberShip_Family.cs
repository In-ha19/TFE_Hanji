namespace Gestionnaire_Collections.Models
{//Classe crée afin que les managers des familles recoivent les demandes d'adhésion
    public class MemberShip_Family
    {
        public int Id { get; set; }  
        public string RequesterId { get; set; } // Utilisateur demandant l’adhésion
        public ApplicationUser Requester { get; set; }
        public string FamilyId { get; set; } // Identifiant de la famille
        public Family Family { get; set; }
        public int Statut { get; set; } // Statut de la demande (0 = En attente, 1 = Rejoint, 2 = Demander à rejoindre)
    }
}
