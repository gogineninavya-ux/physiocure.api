namespace Physiocure.API.DTOs
{
    public class LoginDto
    {
        public string LoginId { get; set; } = string.Empty; // ✅ Email or Mobile
        public string Password { get; set; } = string.Empty;
    }
}
