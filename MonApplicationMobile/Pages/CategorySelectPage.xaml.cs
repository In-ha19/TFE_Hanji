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

        private void OnCategorySelected(object sender, EventArgs e)
        {
            var selectedCategory = categoryPicker.SelectedItem as string;

            Console.WriteLine($"Catégorie sélectionnée : {selectedCategory}");
        }

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
        private async void OnCancelCategory(object sender, EventArgs e)
        {
            tcs.SetResult(null);
            await Navigation.PopModalAsync();
        }
    }

}