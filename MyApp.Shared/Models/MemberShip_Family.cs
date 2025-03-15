namespace Gestionnaire_Collections.Models
{//Classe crée afin que les managers des familles recoivent les demandes d'adhésion
    public class MemberShip_Family
    {
        public int Id { get; set; }  
        public string RequesterId { get; set; }
        public ApplicationUser Requester { get; set; }
        public string FamilyId { get; set; }
        public Family Family { get; set; }
        public int Statut { get; set; }
    }
}
