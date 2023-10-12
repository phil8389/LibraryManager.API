using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManager.API.Models
{
    public partial class Transaction
    {
        public int TxnId { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? LastRenewedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int? TxnStatusId { get; set; }
        public DateTime? StatusDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual TransactionStatus TxnStatus { get; set; }
        public virtual User User { get; set; }
    }
}
