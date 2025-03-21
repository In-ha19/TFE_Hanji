using Microsoft.Maui.Controls;

namespace MonApplicationMobile.Views
{
    public partial class CategorySelectPage : ContentPage
    {
        private List<string> categoryNames;
        private TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
        public Task<string> CategorySelectedTask => tcs.Task;

        public CategorySelectPage(List<string> categories)
        {
            InitializeComponent();

            categoryNames = categories;
            categoryPicker.ItemsSource = categoryNames;
        }
        /**
 * M�thode appel�e lorsqu'une cat�gorie est s�lectionn�e dans le Picker.
 * Affiche la cat�gorie s�lectionn�e dans la console.
 */
        private void OnCategorySelected(object sender, EventArgs e)
        {
            var selectedCategory = categoryPicker.SelectedItem as string;

            Console.WriteLine($"Cat�gorie s�lectionn�e : {selectedCategory}");
        }
        /**
 * M�thode appel�e lors de la validation de la s�lection de cat�gorie.
 * V�rifie si une cat�gorie est s�lectionn�e, la valide et ferme la page modale.
 */
        private async void OnValidateCategory(object sender, EventArgs e)
        {
            var selectedCategory = categoryPicker.SelectedItem as string;

            if (selectedCategory != null)
            {
                tcs.SetResult(selectedCategory);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Erreur", "Veuillez s�lectionner une cat�gorie.", "OK");
            }
        }

        /**
 * M�thode appel�e lorsqu'on annule la s�lection de cat�gorie.
 * Ferme la page modale sans enregistrer de s�lection.
 */
        private async void OnCancelCategory(object sender, EventArgs e)
        {
            tcs.SetResult(null);
            await Navigation.PopModalAsync();
        }
    }

}