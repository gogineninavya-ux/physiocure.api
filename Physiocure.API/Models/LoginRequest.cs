namespace Physiocure.API.Dtos
{
    public class LoginRequest
    {
        public string LoginId { get; set; } = string.Empty; // ✅ email or mobile
        public string Password { get; set; } = string.Empty;
    }
}
