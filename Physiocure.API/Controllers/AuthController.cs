using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Physiocure.API.Data;
using Physiocure.API.Models;
using Physiocure.API.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Physiocure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ==========================
        // ✅ REGISTER (Client Only)
        // ==========================
        [HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterDto dto)
{
    if (string.IsNullOrWhiteSpace(dto.FullName) ||
        string.IsNullOrWhiteSpace(dto.Email) ||
        string.IsNullOrWhiteSpace(dto.Mobile) ||
        string.IsNullOrWhiteSpace(dto.Password))
    {
        return BadRequest(new { message = "All fields are required" });
    }

    var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
    if (emailExists)
        return BadRequest(new { message = "Email already registered" });

    var mobileExists = await _context.Users.AnyAsync(u => u.Mobile == dto.Mobile);
    if (mobileExists)
        return BadRequest(new { message = "Mobile already registered" });

    var user = new User
    {
        FullName = dto.FullName,
        Email = dto.Email,
        Mobile = dto.Mobile,
        Password = dto.Password,
        Role = "Client"
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return Ok(new { message = "Registration successful" });
}

        // ==========================
        // ✅ LOGIN (Client + Admin)
        // ==========================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.LoginId) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "LoginId and Password required" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                (u.Email == request.LoginId || u.Mobile == request.LoginId) &&
                u.Password == request.Password
            );

            if (user == null)
                return Unauthorized(new { message = "Invalid Email/Mobile or Password" });

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                message = "Login successful",
                token = token,
                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.Mobile,
                    user.Role
                }
            });
        }

        // ==========================
        // ✅ JWT Token Generator
        // ==========================
        private string GenerateJwtToken(User user)
        {
            var key = _config["Jwt:Key"] ?? throw new Exception("Jwt:Key is missing in appsettings.json");
            var issuer = _config["Jwt:Issuer"] ?? "PhysiocureAPI";
            var audience = _config["Jwt:Audience"] ?? "PhysiocureClient";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Mobile),
                new Claim(ClaimTypes.Role, user.Role)
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
