using System.ComponentModel.DataAnnotations;

namespace Duckie.Models
{
    public class Milestone
    {
        public int MilestoneID { get; set; } // PK

        [Required]
        [Display(Name = "Milestone")]
        public string MilestoneName { get; set;}

        [Required]
        [Display(Name = "Date of Milestone")]
        public DateTime? MilestoneDate { get; set; }

        public string Comments { get; set; }

        public string Photo { get; set; }

        [Required]
        [Display(Name = "Child")]
        public int ChildProfileId { get; set; }

        // parent reference
        public ChildProfile? ChildProfile { get; set; }
    }
}
