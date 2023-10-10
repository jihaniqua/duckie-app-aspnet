using System.ComponentModel.DataAnnotations;

namespace Duckie.Models
{
    public class Journal
    {
        public int JournalId { get; set; }

        [Display(Name = "Date")]
        public DateTime? JournalDate { get; set; }

        [Required]
        [Display(Name = "Reflections and Musings")]
        public string JournalBody { get; set; }

        [Required]
        [Display(Name = "Child")]
        public int ChildProfileId { get; set; }

        // parent reference
        [Display(Name = "Child")]
        public ChildProfile? ChildProfile { get; set; }
    }
}
