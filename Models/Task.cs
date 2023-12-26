using System.ComponentModel.DataAnnotations;

namespace PTN_BackendAssignment.Models
{
    public class Task
    {
        [Key]
        public int TaskId {  get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Deadline { get; set; }

    }
}
