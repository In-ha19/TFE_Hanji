using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MonApplicationMobile.Models
{
    internal class LocalArticle
    {
        [PrimaryKey]
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Edition { get; set; }  
        public string Autor_name { get; set; }  
        public string CategoryName { get; set; }
    }
}
