using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendlyBills.Models
{
    public class TransactionViewModel
    {
        public TransactionViewModel() {}
        public TransactionViewModel(Transaction tran)
        {
            ID = tran.ID;
            Description = tran.Description;
            MonetaryAmount = tran.MonetaryAmount;
            EntryTimestamp = tran.EntryTimestamp;
            Approved = tran.Approved;
            Submitter = Utilities.FullName(tran.Submitter);
        }

        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "MonetaryAmount")]
        public decimal? MonetaryAmount { get; set; }
        [Display(Name = "NonMonetaryAmount")]
        public string NonMonetaryAmount { get; set; }
        [Display(Name = "Entry Date")]
        public DateTime EntryTimestamp { get; set; }
        [Display(Name = "Transaction Date")]
        public DateTime? AdditionalTimestamp { get; set; }
        [Display(Name = "Entered By")]
        public string Submitter { get; set; }
        [Display(Name = "Group ID")]
        public int GroupID { get; set; }
        [Display(Name = "Approval Status")]
        public bool Approved { get; set; }
    }
}