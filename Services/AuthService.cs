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

        public async Task<string> Login(Login model, LibraryContext context)
        {
            User user = context.User.First(u => u.Username == model.Username);

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    int id = 0;
                    switch (user.Role)
                    {
                        case 1:
                            id = context.Supervisor.First(u => u.UserId == user.Id).Id;
                            break;
                        case 2:
                            id = context.Teacher.First(u => u.UserId == user.Id).Id;
                            break;
                        case 3:
                            id = context.Student.First(u => u.UserId == user.Id).Id;
                            break;
                    }
                    return GenerateJwtToken(user.Role, id);
                }
                return null;
            }
            return null;
        }

        public string GenerateJwtToken(int role, int id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim("userId", id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["key"]));
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
