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
 * Méthode appelée lorsqu'une catégorie est sélectionnée dans le Picker.
 * Affiche la catégorie sélectionnée dans la console.
 */
        private void OnCategorySelected(object sender, EventArgs e)
        {
            var selectedCategory = categoryPicker.SelectedItem as string;

            Console.WriteLine($"Catégorie sélectionnée : {selectedCategory}");
        }
        /**
 * Méthode appelée lors de la validation de la sélection de catégorie.
 * Vérifie si une catégorie est sélectionnée, la valide et ferme la page modale.
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
                await DisplayAlert("Erreur", "Veuillez sélectionner une catégorie.", "OK");
            }
        }

        /**
 * Méthode appelée lorsqu'on annule la sélection de catégorie.
 * Ferme la page modale sans enregistrer de sélection.
 */
        private async void OnCancelCategory(object sender, EventArgs e)
        {
            tcs.SetResult(null);
            await Navigation.PopModalAsync();
        }
    }

}