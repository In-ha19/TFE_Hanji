using Microsoft.Maui.Storage;  // Pour accéder au FileSystem
using MonApplicationMobile;
using MyApp.Shared.Services; 
using MonApplicationMobile.Views;

namespace MonApplicationMobile
{
    public partial class App : Application
    {
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, "collection.db");
        public App()
        {
            InitializeComponent();

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
        protected override void OnSleep()
        {
            base.OnSleep();

            Preferences.Remove("UserLogin");
        }

        private bool IsInternetAvailable()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

    }
}
