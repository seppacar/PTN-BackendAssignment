using System.ComponentModel.DataAnnotations;

namespace PTN_BackendAssignment.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
