using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Physiocure.API.Data;
using Physiocure.API.Models;

namespace Physiocure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminAuthController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ TEST
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("AdminAuth Working");
        }

        // ✅ LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginDto dto)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == dto.Email && a.Password == dto.Password);

            if (admin == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(new
            {
                message = "Admin login success",
                adminId = admin.Id,
                adminName = admin.Name,
                adminEmail = admin.Email
            });
        }
    }
}
