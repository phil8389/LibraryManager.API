﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.API.Models.Dto
{
    public class CheckoutDto
    {
        public int TxnId { get; set; }
        public int? BookId { get; set; }
        public int ISBN { get; set; }
        public string Author { get; set; }
        public string BookTitle { get; set; }
        public int? UserId { get; set; }
        public string UserFullName { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? LastRenewedDate { get; set; }
        public int? RenewalCount { get; set; }


        public bool? IsOverDue
        {
            get
            {
                if (this.DueDate == null)
                {
                    return null;
                }
                else
                {
                    if (this.DueDate < DateTime.Today)
                    {
                        return true;

                    }
                    else { return false; }
                }
            }

        }
    }

    // public DateTime ReturnedDate { get; set; }
    // public DateTime dateTime { get; set; }

}
