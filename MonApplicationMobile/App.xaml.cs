using Microsoft.Maui.Storage;  // Pour accéder au FileSystem
using MonApplicationMobile;
using MyApp.Shared.Services; 
using MonApplicationMobile.Views;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;
using Microsoft.Maui.Networking;

namespace MonApplicationMobile
{
    public partial class App : Application
    {
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, "collection.db");
        public static string ServerIP { get; private set; } = "http://localhost:7054"; //valeur par défault
        public App()
        {
            InitializeComponent();

            Task.Run(async () => await LoadConfiguration()).Wait();

            if (File.Exists(DatabasePath))
            {
                //File.Delete(DatabasePath);
            }

            var localDataService = new LocalDataService(DatabasePath);
            localDataService.InitializeDatabase();

            try
            {
                if (!IsInternetAvailable())
                {
                    MainPage = new NavigationPage(new MesCollectionsPage());
                }
                else
                {
                    MainPage = new NavigationPage(new MainPage());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(  ex.Message);
            }
        }

        private async Task LoadConfiguration()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("config.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                var config = JsonSerializer.Deserialize<ConfigData>(json);
                if (config != null && !string.IsNullOrEmpty(config.ServerIP))
                {
                    ServerIP = config.ServerIP;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de lecture du fichier de configuration : {ex.Message}");
            }
        }
        protected override void OnSleep()
        {
            base.OnSleep();

            Preferences.Remove("UserLogin");
        }

        private bool IsInternetAvailable()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        // Classe pour le modèle de configuration
        public class ConfigData
        {
            public string ServerIP { get; set; }
        }

    }
}
