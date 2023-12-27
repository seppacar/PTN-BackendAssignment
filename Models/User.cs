using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PTN_BackendAssignment.Models
{
    // Represents a user in the application
    public class User
    {
        // Primary key for the user
        [Key]
        public int Id { get; set; }

        // Required: Email of the user with email format validation
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Required: Password hash of the user
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        // Date and time when the user was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property to the tasks owned by the user (JsonIgnore to avoid circular serialization)
        [JsonIgnore]
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
