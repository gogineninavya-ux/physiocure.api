using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Physiocure.API.Data;
using Physiocure.API.Models;
using Physiocure.API.Dtos;

namespace Physiocure.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminAuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(AdminForgotPasswordDto dto)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Mobile == dto.Mobile);

            if (admin == null)
                return NotFound(new { message = "Admin mobile not found" });

            admin.Password = dto.NewPassword;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Password updated successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginDto dto)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Mobile == dto.Mobile && a.Password == dto.Password);

            if (admin == null)
                return Unauthorized(new { message = "Invalid mobile or password" });

            return Ok(new { message = "Admin login success", adminId = admin.Id });
        }
    }
}
