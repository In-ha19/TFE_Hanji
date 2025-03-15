using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MonApplicationMobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/"); // L'URL de ton API
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginRequest = new { Username = username, Password = password };
            var jsonContent = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                return true; // Connexion réussie
            }

            return false; // Échec de la connexion
        }
    }
}
