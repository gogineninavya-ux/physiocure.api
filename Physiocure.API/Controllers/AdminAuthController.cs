using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Physiocure.API.Data;
using Physiocure.API.Dtos;
using Physiocure.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Physiocure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AdminAuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("AdminAuth Working");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { message = "Email and Password required" });

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == dto.Email && a.Password == dto.Password);

            if (admin == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var token = GenerateAdminJwtToken(admin);

            return Ok(new
            {
                message = "Admin login success",
                token = token,
                admin = new
                {
                    admin.Id,
                    admin.Name,
                    admin.Email,
                    role = "Admin"
                }
            });
        }

        private string GenerateAdminJwtToken(Admin admin)
{
    var key = _config["Jwt:Key"] ?? throw new Exception("Jwt:Key missing in appsettings.json");
    var issuer = _config["Jwt:Issuer"] ?? "PhysiocureAPI";
    var audience = _config["Jwt:Audience"] ?? "PhysiocureClient";

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
        new Claim(ClaimTypes.Name, admin.Name ?? ""),
        new Claim(ClaimTypes.Email, admin.Email ?? ""),
        new Claim(ClaimTypes.Role, "Admin")
    };

    var token = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        claims: claims,
        expires: DateTime.Now.AddHours(2),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
    }
}