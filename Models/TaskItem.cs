using System.ComponentModel.DataAnnotations;

namespace PTN_BackendAssignment.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public TaskType TaskType { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum TaskType
    {
        Daily,
        Weekly,
        Monthly,
    }
}
