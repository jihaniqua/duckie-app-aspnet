using System.ComponentModel.DataAnnotations;

namespace Duckie.Models
{
    public class ChildProfile
    {
        public int ChildProfileId { get; set; } // PK

        [Required]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        // child references
        public List<Milestone>? Milestones { get; set; }
    }
}
