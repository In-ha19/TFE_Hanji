using System.Net.Http;
using Newtonsoft.Json;
using ZXing.Net.Maui.Controls;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using ZXing.Net.Maui;
using System.Text;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using MyApp.Shared;
using Microsoft.EntityFrameworkCore;
using MonApplicationMobile.Models;
using MonApplicationMobile.Data;
using System.Diagnostics;
using MonApplicationMobile.Views;


namespace MonApplicationMobile
{
    public partial class MainPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        private HubConnection _connection;
        private bool isScannerVisible = false;
        private ScanBooks.BookDetails bookDetails;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            //string url = "https://www.youtube.com/";

#if ANDROID
            if (webView.Handler?.PlatformView is Android.Webkit.WebView androidWebView)
            {              
                androidWebView.SetWebViewClient(new MonApplicationMobile.Platforms.Android.CustomWebViewClient());
            }
#endif
            //SetupSignalRConnection();

            ///!!!!! en http en production mettre https
            string url = "http://192.168.0.23:7054";
            webView.Source = url;

            cameraBarcodeReaderView.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                Formats = ZXing.Net.Maui.BarcodeFormat.Ean13, 
                AutoRotate = true,
                Multiple = false 
            };
        }
        /**
 * Méthode appelée lors de la navigation dans le WebView.
 * Elle vérifie l'URL pour décider si la navigation doit être autorisée ou annulée.
 */
        private void OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.StartsWith("https://"))
            {
                e.Cancel = false;  
            }
        }
        private async void OnSyncButtonClicked(object sender, EventArgs e)
        {
            string username = Preferences.Get("UserLogin", null);

            if (string.IsNullOrEmpty(username))
            {
                var loginPage = new Views.LoginPage();
                await Navigation.PushModalAsync(loginPage);

                await loginPage.LoginTaskCompletionSource.Task;

                username = Preferences.Get("UserLogin", null);
                if (string.IsNullOrEmpty(username))
                {
                    await DisplayAlert("Erreur", "Vous devez être connecté pour synchroniser.", "OK");
                    return;
                }
            }

            var categories = await GetCategoriesFromApi();

            if (categories != null && categories.Count > 0)
            {
                var categorySelectPage = new Views.CategoryForSynchro(categories);
                await Navigation.PushModalAsync(categorySelectPage);

                var selectedCategories = await categorySelectPage.CategoriesSelectedTask;

                if (selectedCategories != null && selectedCategories.Count > 0)
                {
                    //await SyncMyCollections(selectedCategories);
                }
            }
        }
        private void OnScanButtonClicked(object sender, EventArgs e)
        {
            isScannerVisible = !isScannerVisible;

            cameraFrame.IsVisible = !cameraFrame.IsVisible;
            cameraBarcodeReaderView.IsDetecting = cameraFrame.IsVisible;
            cameraBarcodeReaderView.IsVisible = cameraFrame.IsVisible;

            Console.WriteLine($"Scanner visible : {cameraBarcodeReaderView.IsVisible}");
            Console.WriteLine($"Scanner détecte : {cameraBarcodeReaderView.IsDetecting}");
        }

        /**
 * Méthode appelée après qu'une navigation dans le WebView soit terminée.
 * Elle vérifie le résultat de la navigation et affiche un message d'erreur si la navigation a échoué.
 */
        private void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Result == WebNavigationResult.Failure)
            {
                DisplayAlert("Erreur", "La page n'a pas pu être chargée. Vérifiez la connexion.", "OK");
            }
        }
        private async void BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            var first = e.Results?.FirstOrDefault();
            if (first == null)
            {
                return;
            }

            string barcode = first.Value;

            bookDetails = await GetBookDetails(barcode);

            MainThread.BeginInvokeOnMainThread(() =>
            {               
                if (bookDetails != null)
                {
                    modalMessage.Text = $"Titre: {bookDetails.Title}\nAuteur: {bookDetails.Author}\nISBN: {bookDetails.ISBN}\nEdition: {bookDetails.Edition}";
                    addButton.IsVisible = true;  
                }
                else
                {
                    modalMessage.Text = "Livre introuvable";
                    addButton.IsVisible = false; 
                }

                modalGrid.IsVisible = true;

                isScannerVisible = false;
                cameraBarcodeReaderView.IsVisible = false;
            });           
        }
        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            modalGrid.IsVisible = false;
        }
        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            string username = Preferences.Get("UserLogin", null);

            if (string.IsNullOrEmpty(username))
            {
                var loginPage = new Views.LoginPage();
                await Navigation.PushModalAsync(loginPage);

                await loginPage.LoginTaskCompletionSource.Task;

                username = Preferences.Get("UserLogin", null);
                if (string.IsNullOrEmpty(username))
                {
                    await DisplayAlert("Erreur", "Vous devez être connecté pour synchroniser.", "OK");
                    return;
                }
            }           
                if (bookDetails != null)
                {
                    var categories = await GetCategoriesFromApi();

                    if (categories != null && categories.Count > 0)
                    {
                        var categorySelectPage = new Views.CategorySelectPage(categories);
                        await Navigation.PushModalAsync(categorySelectPage);

                        var selectedCategory = await categorySelectPage.CategorySelectedTask;

                        if (selectedCategory != null)
                        {
                            var addBookRequest = new AddBookToCollectionRequest
                            {
                                ISBN = bookDetails.ISBN,
                                Title = bookDetails.Title,
                                Author = bookDetails.Author,
                                Edition = bookDetails.Edition,
                                CoverUrl = bookDetails.CoverUrl,
                                Login = username,
                                Category = selectedCategory 
                            };

                            string apiMessage = await AddBookToCollection(addBookRequest);

                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                modalGrid.IsVisible = true;
                                addButton.IsVisible = false;
                                modalMessage.Text = apiMessage;
                            });
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "Aucune catégorie sélectionnée.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Aucune catégorie disponible.", "OK");
                    }
                }
                else
                {
                    modalMessage.Text = "Détails du livre non disponibles.";
                }           
        }
        private async Task<List<string>> GetCategoriesFromApi()
        {
            var httpClient = new HttpClient();
            //Attention http --> https
            var response = await httpClient.GetAsync("http://192.168.0.23:7054/api/categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                //Console.WriteLine("Réponse de l'API : " + jsonContent);

                var categories = JsonConvert.DeserializeObject<List<string>>(jsonContent);

                return categories;
            }
            else
            {
                return new List<string>();
            }
        }
        private async Task<ScanBooks.BookDetails> GetBookDetails(string barcode)
        {
            try
            {
                var response = await client.GetStringAsync($"https://openlibrary.org/api/books?bibkeys=ISBN:{barcode}&format=json&jscmd=data");

                var bookData = JsonConvert.DeserializeObject<Dictionary<string, ScanBooks.BookInfo>>(response);

                if (bookData != null && bookData.ContainsKey($"ISBN:{barcode}"))
                {
                    var book = bookData[$"ISBN:{barcode}"];
                    return new ScanBooks.BookDetails
                    {
                        ISBN = barcode,
                        Title = book.Title,
                        Author = string.Join(", ", book.Authors?.Select(a => a.Name) ?? new List<string>()),
                        Edition = book.Publishers?.FirstOrDefault()?.Name
                    };
                }
                else
                {
                    Console.WriteLine("Livre introuvable pour ce code-barres.");
                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des informations du livre: {ex.Message}");
            }

            return null;
        }
        private async Task<string> AddBookToCollection(AddBookToCollectionRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                Console.WriteLine($"Request body: {json}");
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //!!! attention  https en production
                var response = await client.PostAsync("http://192.168.0.23:7054/api/articles", content);
      
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(result);

                    return jsonResponse.GetProperty("message").GetString();
                }
                else
                {
                    Console.WriteLine($"Erreur lors de l'ajout du livre: {response.StatusCode}");
                    return $"Erreur lors de l'ajout du livre dans la collection: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'ajout du livre à la collection: {ex.Message}");
                return $"Erreur lors de l'ajout du livre: {ex.Message}";
            }
        }
        // Classe pour stocker les informations du livre
        public class AddBookToCollectionRequest
        {
            public string ISBN { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Edition { get; set; }
            public string CoverUrl { get; set; }
            public string Login { get; set; }
            public string Category { get; set; }
        }

        /*public class BookDetails
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string CoverUrl { get; set; }
            public string Edition { get; set; }
            public string ISBN { get; set; }
        }

        public class BookResponse
        {
            public BookInfo ISBN { get; set; }
        }

        public class BookInfo
        {
            public string Title { get; set; }
            public List<Author> Authors { get; set; }
            public Cover Cover { get; set; }
            public List<Publisher> Publishers { get; set; }
        }
        public class Publisher
        {
            public string Name { get; set; } 
        }
        public class Author
        {
            public string Name { get; set; }
        }

        public class Cover
        {
            public string Small { get; set; }
            public string Medium { get; set; }
            public string Large { get; set; }
        }
        */       
    }
}
