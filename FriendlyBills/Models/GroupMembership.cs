using FriendlyBills.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendlyBills.Models
{
    public class GroupMembership
    {
        public int ID { get; set; }
        public int Rank { get; set; }
        public int GroupID { get; set; }
        public string UserID { get; set; }

        public virtual Group Group { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}