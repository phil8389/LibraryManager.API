using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManager.API.Models
{
    public partial class User
    {
        public User()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
