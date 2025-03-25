using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MonApplicationMobile.Models;

namespace MonApplicationMobile.Views
{
    public partial class CategoryForSynchro : ContentPage
    {
        public List<string> categoryNames { get; set; }
        private TaskCompletionSource<List<string>> tcs = new TaskCompletionSource<List<string>>();
        public Task<List<string>> CategoriesSelectedTask => tcs.Task;

        //private readonly string apiBaseUrl = "http://192.168.0.23:7054/api/UserArticles/GetUserArticles";  
       
        private readonly string apiBaseUrl = $"{App.ServerIP}/api/UserArticles/GetUserArticles";
        public CategoryForSynchro(List<string> categories)
        {
            InitializeComponent();
            categoryNames = categories;
            BindingContext = this;
        }

        /**
 * M�thode appel�e lorsqu'on valide la s�lection des cat�gories.
 * Elle r�cup�re les cat�gories s�lectionn�es, lance la synchronisation et ferme la page modale.
 */
        private async void OnValidateCategory(object sender, EventArgs e)
        {
            var selectedCategories = categoryCollectionView.SelectedItems.Cast<string>().ToList();

            if (selectedCategories.Any())
            {
                tcs.SetResult(selectedCategories);
                await SynchronizeDataAsync(selectedCategories);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Erreur", "Veuillez s�lectionner une ou plusieurs cat�gories.", "OK");
            }
        }
        /**
 * M�thode appel�e lorsqu'on annule la s�lection des cat�gories.
 * Elle ferme la page modale sans enregistrer de s�lection.
 */
        private async void OnCancelCategory(object sender, EventArgs e)
        {
            tcs.SetResult(null);
            await Navigation.PopModalAsync();
        }
        /**
 * Synchronise les articles li�s aux cat�gories s�lectionn�es avec le serveur.
 */
        private async Task SynchronizeDataAsync(List<string> selectedCategories)
        {
            string username = Preferences.Get("UserLogin", null);

            if (string.IsNullOrEmpty(username))
            {
                await DisplayAlert("Erreur", "Utilisateur non authentifi�.", "OK");
                return;
            }

            try
            {
                var requestData = new
                {
                    Login = username,
                    Categories = selectedCategories
                };

                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var client = new HttpClient();
                var response = await client.PostAsync($"{apiBaseUrl}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var articles = JsonConvert.DeserializeObject<List<Article>>(responseBody);  

                    if (articles != null)
                    {
                        await DeleteLocalDataAsync();

                        await InsertArticlesToLocalDatabaseAsync(articles);
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Aucun article trouv� pour les cat�gories s�lectionn�es.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Erreur", "Une erreur est survenue lors de la synchronisation. V�rifiez si des articles sont pr�sents dans votre collection pour les cat�gories choisies.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", "Impossible de contacter le serveur : " + ex.Message, "OK");
            }
        }

        private async Task DeleteLocalDataAsync()
        {
            using var db = new SQLite.SQLiteConnection(App.DatabasePath);
            db.DeleteAll<LocalArticle>(); 
        }

        private async Task InsertArticlesToLocalDatabaseAsync(List<Article> articles)
        {
            using var db = new SQLite.SQLiteConnection(App.DatabasePath);

            var localArticles = articles.Select(article => new LocalArticle
            {
                Id = article.Id,
                Name = article.Name,
                Edition = article.Edition,
                Autor_name = article.Autor_name,
                CategoryName = article.CategoryName
            }).ToList();

            db.InsertAll(localArticles);
        }
    }
    public class Article
    {
        public string Id { get; set; }
        public string Name { get; set; } 
        public string Edition { get; set; }  
        public string Autor_name { get; set; }  
        public string CategoryName { get; set; }
    }
}
