namespace Physiocure.API.Dtos
{
    public class AdminForgotPasswordDto
    {
        public string Mobile { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
