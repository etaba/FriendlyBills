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
        }

        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}