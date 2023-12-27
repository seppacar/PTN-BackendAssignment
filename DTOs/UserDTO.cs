using System.Text.Json.Serialization;

namespace PTN_BackendAssignment.DTOs
{
    public class UserDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
