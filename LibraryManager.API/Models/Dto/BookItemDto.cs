using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.API.Models.Dto
{
    public class BookItemDto
    {        
        public int BookId { get; set; }
        public int ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int? PublishedYear { get; set; }
        public string Publisher { get; set; }
        public decimal? Price { get; set; }

        public int TxnId { get; set; }
        public int? UserId { get; set; }
        public string UserFullName { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? LastRenewedDate { get; set; }
        public int? RenewalCount { get; set; }

    }
}
