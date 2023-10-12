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
        public string Titile { get; set; }
        
    }
}
