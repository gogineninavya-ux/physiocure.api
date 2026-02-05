using System.ComponentModel.DataAnnotations;

namespace Physiocure.API.DTOs
{
    public class CreateAdminDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Mobile { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
