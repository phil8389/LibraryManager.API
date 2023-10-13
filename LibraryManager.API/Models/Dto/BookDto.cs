using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.API.Models.Dto
{
    public class BookDto
    {        
        public int BookId { get; set; }
        public int ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int? PublishedYear { get; set; }
        public string Publisher { get; set; }
        public decimal? Price { get; set; }       
    }
}
