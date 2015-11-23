using FriendlyBills.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FriendlyBills.Models
{
    public class GroupMembership
    {
        //[Key]
        public int ID { get; set; }
        public int Rank { get; set; }
        [Required]
        public int GroupID { get; set; }
        [Required]
        public string UserID { get; set; }

        [ForeignKey("GroupID")]
        public virtual Group Group { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}