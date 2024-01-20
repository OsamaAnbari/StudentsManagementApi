using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Students_Management_Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Students_Management_Api.Services
{
    public class AuthService
    {
        private IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Login(string role, string UserId, LibraryContext context)
        {
            if (UserId != null)
            {
                int id = 0;
                switch (role)
                {
                    case "Supervisor":
                        id = context.Supervisor.First(u => u.UserId == UserId).Id;
                        break;
                    case "Teacher":
                        id = context.Teacher.First(u => u.UserId == UserId).Id;
                        break;
                    case "Student":
                        id = context.Student.First(u => u.UserId == UserId).Id;
                        break;
                }
                return GenerateJwtToken(role, id.ToString());
            }
            return null;
        }

        public string GenerateJwtToken(string role, string id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
