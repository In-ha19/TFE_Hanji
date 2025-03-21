using Microsoft.Maui.Controls;
using MonApplicationMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonApplicationMobile.Views
{
    public partial class MesCollectionsPage : ContentPage
    {
        private SQLiteAsyncConnection _database;
        private List<LocalArticle> _allArticles;

        public MesCollectionsPage()
        {
            InitializeComponent();
            _database = new SQLiteAsyncConnection(App.DatabasePath);
        }

        /**
 * M�thode appel�e automatiquement lorsque la page appara�t � l'�cran.
 * Elle charge les donn�es des articles � partir de la base locale.
 */
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        /**
 * Charge les articles depuis la base de donn�es locale et remplit la liste des cat�gories uniques.
 */
        private async Task LoadData()
        {
            _allArticles = await _database.Table<LocalArticle>().ToListAsync();

            var categories = _allArticles.Select(a => a.CategoryName).Distinct().OrderBy(c => c).ToList();

            CategoryPicker.ItemsSource = categories;
        }

        /**
       * M�thode appel�e lorsqu'une cat�gorie est s�lectionn�e dans le Picker.
       * Elle filtre les articles en fonction de la cat�gorie choisie.
       */
        private void OnCategorySelected(object sender, EventArgs e)
        {
            if (CategoryPicker.SelectedItem is string selectedCategory)
            {
                var filteredArticles = _allArticles.Where(a => a.CategoryName == selectedCategory).ToList();

                ArticlesCollectionView.ItemsSource = filteredArticles;

                NoArticlesLabel.IsVisible = filteredArticles.Count == 0;
            }
        }
    }
}
