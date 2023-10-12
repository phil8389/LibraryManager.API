using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManager.API.Models
{
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int StatusId { get; set; }
        public string StatusDesc { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
