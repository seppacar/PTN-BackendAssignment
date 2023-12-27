using System.Text.Json.Serialization;

namespace PTN_BackendAssignment.DTOs
{
    /// <summary>
    /// Represents user data for external communication.
    /// </summary>
    public class UserDTO
    {
        [JsonIgnore] public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
