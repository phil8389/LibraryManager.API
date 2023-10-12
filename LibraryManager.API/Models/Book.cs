using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManager.API.Models
{
    public partial class Book
    {
        public Book()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int BookId { get; set; }
        public int Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? PublishedYear { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
