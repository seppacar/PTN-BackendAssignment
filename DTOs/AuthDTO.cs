namespace PTN_BackendAssignment.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for authentication purposes.
    /// </summary>
    public class AuthDTO
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
