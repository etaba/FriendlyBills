using System.ComponentModel.DataAnnotations;

namespace FriendlyBills.Models
{
    public class CreateGroupViewModel
    {
        public CreateGroupViewModel() { Id = -1; Name = null; }
        public CreateGroupViewModel(Group group)
        {
            Id = group.ID;
            Name = group.Name;
            Description = group.Description;
        }

        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        public Dictionary<string, int> MemberBalances {get;} 
        
    }
}