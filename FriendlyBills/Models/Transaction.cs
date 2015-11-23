using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FriendlyBills.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public decimal? MonetaryAmount { get; set; }
        public string NonMonetaryAmount { get; set; }
        public DateTime EntryTimestamp {get; set;}
        public DateTime? AdditionalTimestamp { get; set; }
        [Required]
        public string SubmitterID {get; set;}
        [Required]
        public string TargetUserID { get; set; }
        [Required]
        public int GroupID { get; set; }
        public bool Approved { get; set; }

        [ForeignKey("SubmitterID")]
        public virtual ApplicationUser Submitter { get; set; }
        [ForeignKey("TargetUserID")]
        public virtual ApplicationUser TargetUser { get; set; }
        [ForeignKey("GroupID")]
        public virtual Group Group { get; set; }
    }
}