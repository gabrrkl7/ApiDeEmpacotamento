using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using LojaDoManoel.Api.Models;
using LojaDoManoel.Api.Models.Auth;
using LojaDoManoel.Api.Data;
using LojaDoManoel.Api.Services.Interfaces;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace LojaDoManoel.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly LojaDoManoelDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(LojaDoManoelDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return new AuthResponse
            {
                Token = GenerateJwtToken(user),
                Username = user.Username,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task CreateAdminUserAsync()
        {
            if (!await _context.Users.AnyAsync(u => u.Username == "admin"))
            {
                var adminUser = new User
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123", 13),
                    Role = "Admin"
                };

                _context.Users.Add(adminUser);
                await _context.SaveChangesAsync();
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
