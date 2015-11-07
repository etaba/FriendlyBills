using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendlyBills.Models
{
    public class Transaction
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public decimal? MonetaryAmount { get; set; }
        public string NonMonetaryAmount { get; set; }
        public DateTime EntryTimestamp {get; set;}
        public DateTime? AdditionalTimestamp { get; set; }
        public string SubmitterID {get; set;}
        public string TargetID { get; set; }
        public int GroupID { get; set; }
        public bool Approved { get; set; }

        [Required]
        public virtual ApplicationUser Submitter { get; set; }
        [Required]
        public virtual ApplicationUser TargetUser { get; set; }
        [Required]
        public virtual Group Group { get; set; }
    }
}