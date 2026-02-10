namespace Physiocure.API.Dtos
{
    public class ClientResetPasswordDto
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
