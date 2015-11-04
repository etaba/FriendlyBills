using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FriendlyBills.Models
{
    public class GroupViewModel
    {
        public GroupViewModel() { Id = -1; Name = null; }
        public GroupViewModel(Group group,
                              Dictionary<string, decimal> memberBalances)
        {
            Id = group.ID;
            Name = group.Name;
            Description = group.Description;
            MemberBalances = memberBalances;
        }

        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        public Dictionary<string, decimal> MemberBalances { get; set; }        
    }
}