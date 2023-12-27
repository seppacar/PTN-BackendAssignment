using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PTN_BackendAssignment.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt = DateTime.Now;

        [JsonIgnore]
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
