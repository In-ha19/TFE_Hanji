using Gestionnaire_Collections.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestionnaire_Collections.ViewModels
{
    public class ArticleViewModelCreate_Edit
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = "Le Nom est obligatoire.")]
        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Edition")]
        public string? Edition { get; set; }
        [Display(Name = "ISBN")]
        public string? Isbn { get; set; }
        [Display(Name = "Nom des auteurs")]
        public string? Autor_name { get; set; }
        [Display(Name = "Date de parution")]
        public string? Date { get; set; }
        [Display(Name = "Résumé")]
        public string? Summary { get; set; }
        [Display(Name = "Prix")]
        //Regular Expression (Regex) => Fait matcher une string par rapport à un pattern
        //Ici, le pattern [0-9] signifie "un caractère doit être compris entre les car 0 à 9
        // l'ajout de |, et |. permet de dire "0 à 9 ou la virgule ou le point"
        //{0,10} signifie que le pattern précédent doit être présent 0 à 10 fois
        //(donc, attention, pas parfaite. Quelqu'un peut mettre plusieurs virgules ou points
        //l'accent circonflexe (^) permet de dire "doit commencer par"
        //la regex complète ^[0-9|,|.]{0,10} signifie "je dois commencer par 0 à 10 caractères compris dans la liste de 0 à 9 ou , ou .
        //Regex testables sur regex101.com
        [RegularExpression(@"^(\d+([.,]\d{1,2})?)?$", ErrorMessage = "Le prix doit être un nombre valide avec au plus deux décimales.")]
        public string? Price { get; set; }

        [Display(Name = "Image")]
        public string? ImageRoot { get; set; }

        //public List<Category_Article> Category_Articles { get; set; } = new List<Category_Article>();
        public string SelectedCategoryId { get; set; } = string.Empty; 
        public List<string>? SelectedCategoryIdsSecondary { get; set; } = new List<string>();
    }
}
