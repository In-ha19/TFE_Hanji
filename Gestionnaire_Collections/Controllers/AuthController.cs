using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Gestionnaire_Collections.Models;

namespace MonApplicationWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Unauthorized("Utilisateur introuvable");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return Ok("Connexion réussie");
            }

            return Unauthorized("Mot de passe incorrect");
        }

        [HttpGet("check-user/{username}")]
        public async Task<IActionResult> CheckUserExists(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("Utilisateur introuvable");
            }

            return Ok("Utilisateur valide");
        }

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
