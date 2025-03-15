using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonApplicationMobile.Models
{
    class ScanBooks
    {
        public class BookDetails
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
    }
}
