using Gestionnaire_Collections.Models;

namespace Gestionnaire_Collections.ViewModels
{
    public class FamilyLeave
    {
        public Family Family { get; set; } = new();
        public bool IsUserManagerInAnyFamily { get; set; }
        public List<ApplicationUser> PotentialManagers { get; set; } = new List<ApplicationUser>();
    }
}
