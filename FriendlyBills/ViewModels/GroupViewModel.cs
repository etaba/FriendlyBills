using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FriendlyBills.Models
{
    public class GroupViewModel
    {
        public GroupViewModel() { Id = -1; Name = null; }
        public GroupViewModel(Group group,
                              List<MemberDetail> memberDetails)
        {
            Id = group.ID;
            Name = group.Name;
            Description = group.Description;
            MemberDetails = memberDetails;
        }

        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        public List<MemberDetail> MemberDetails {get; set;}
             
    }

    public struct MemberDetail
    {
        public string UserID;
        public string FullName;
        public decimal Balance;
    }
}