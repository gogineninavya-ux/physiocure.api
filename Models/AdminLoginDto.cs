namespace Physiocure.API.Models
{
    public class AdminLoginDto
    {
        public string Email { get; set; } = string.Empty;   // ✅ ADD THIS
        public string Password { get; set; } = string.Empty;
    }
}
