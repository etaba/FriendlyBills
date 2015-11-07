using FriendlyBills.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendlyBills.Models
{
    public class Group
    {
        public Group() { ID = -1; Name = null; }
        public Group(FriendlyBills.Models.GroupViewModel grp)
        {
            Name = grp.Name;
        }
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GroupMembership> GroupMemberships { get; set; }
    }
}