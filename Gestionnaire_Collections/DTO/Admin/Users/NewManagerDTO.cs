using Microsoft.AspNetCore.Authentication;

namespace Gestionnaire_Collections.DTO.Admin.Users
{
    public class NewManagerDTO
    {
        public List<FamilleDTO> Familles { get; set; } = new ();
    }
    public class FamilleDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public List<UserDTO> Users { get; set; } = new List<UserDTO>();
    }
    public class  UserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
    }
    public class UserFamily
    {
        public string UserId { get; set; } = string.Empty;
        public string FamilyId { get; set; } = string.Empty;
    }
}
