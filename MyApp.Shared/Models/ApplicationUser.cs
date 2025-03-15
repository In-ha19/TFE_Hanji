using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class ApplicationUser : IdentityUser
{
    // Champs supplémentaires
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public string Login { get; set; } = string.Empty;

    public List<Family_User> Family_Users { get; set; } = new List<Family_User>();
    public List<Collection> Collections { get; set; } = new List<Collection>();
    public List<Notepad> Notepads { get; set; } = new List<Notepad>();
    public List<Message> Messages { get; set; } = new List<Message>();
}
