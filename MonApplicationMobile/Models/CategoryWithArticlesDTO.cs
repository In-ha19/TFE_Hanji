using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonApplicationMobile.Models
{
    class CategoryWithArticlesDTO
    {
        
            public string CategoryName { get; set; }
            public List<ArticleDTO> Articles { get; set; }       

        public class ArticleDTO
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string? Edition { get; set; }
            public string? Autor_name { get; set; }
            public string? Date { get; set; }
        }
    }
}
