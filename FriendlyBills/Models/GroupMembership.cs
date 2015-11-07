using FriendlyBills.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendlyBills.Models
{
    public class GroupMembership
    {
        [Key]
        public int ID { get; set; }
        public int Rank { get; set; }
        public int GroupID { get; set; }
        public string UserID { get; set; }

        [Required]
        public virtual Group Group { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}