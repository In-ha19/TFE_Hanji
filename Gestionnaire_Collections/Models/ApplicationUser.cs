using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class ApplicationUser : IdentityUser
{
    // Champs supplémentaires
    public bool IsAdmin { get; set; } // Indique si l'utilisateur est administrateur
    public bool IsActive { get; set; } // Indique si le compte est actif
    public string Login { get; set; } = string.Empty; // Identifiant unique de connexion

    public List<Family_User> Family_Users { get; set; } = new List<Family_User>();
    public List<Collection> Collections { get; set; } = new List<Collection>();
    public List<Notepad> Notepads { get; set; } = new List<Notepad>();
    public List<Message> Messages { get; set; } = new List<Message>();
}
