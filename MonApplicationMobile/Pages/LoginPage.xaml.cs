using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MonApplicationMobile.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        public TaskCompletionSource<bool> LoginTaskCompletionSource { get; private set; }

        public LoginPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            LoginTaskCompletionSource = new TaskCompletionSource<bool>();
        }

        /**
 * Méthode déclenchée lorsqu'on clique sur le bouton de connexion.
 * Vérifie que l'utilisateur a entré un login valide et tente une authentification via une API.
 */
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = LoginEntry.Text?.Trim();

            if (string.IsNullOrEmpty(username))
            {
                await DisplayAlert("Erreur", "Veuillez entrer votre login.", "OK");
                return;
            }

            bool isValid = await CheckLoginWithApi(username);

            if (!isValid)
            {
                await DisplayAlert("Erreur", "Login incorrect. Veuillez réessayer.", "OK");
                return;
            }

            Preferences.Set("UserLogin", username);

            LoginTaskCompletionSource.SetResult(true);

            await Navigation.PopModalAsync();
        }

        private async Task<bool> CheckLoginWithApi(string username)
        {
            try
            {
                
                //string apiUrl = $"http://192.168.0.23:7054/api/auth/check-user/{username}"; 
                string apiUrl = $"{App.ServerIP}/api/auth/check-user/{username}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                return response.IsSuccessStatusCode; 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", "Impossible de contacter le serveur. Vérifiez votre connexion.", "OK");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
