using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Students_Management_Api.Services;
using Students_Management_Api.Models;

namespace Students_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthController(LibraryContext context)
        {
            _context = context;
        }

        [Route("user")]
        [HttpPost()]
        public async Task<IActionResult> UserLogin(Login model)
        {
            AuthService userService = new AuthService();
            User auth = await userService.Login(model, _context);

            if (auth != null)
            {
                int id=0;
                switch (auth.Role)
                {
                    case 1:
                        id = _context.Supervisor.First(u => u.UserId == auth.Id).Id;
                        break;
                    case 2:
                        id = _context.Teacher.First(u => u.UserId == auth.Id).Id;
                        break;
                    case 3:
                        id = _context.Student.First(u => u.UserId == auth.Id).Id;
                        break;
                }
                var token = GenerateJwtToken(auth.Role, id);
                return Ok(new { token });
            }
            return BadRequest(new { message = "Username or Password is wrong"});
        }

        [Route("admin")]
        [HttpPost()]
        public async Task<IActionResult> AdminLogin(Login model)
        {

            if (model.Username == "osama" && model.Password == "123123")
            {
                var token = GenerateJwtToken(0, 0);
                return Ok(new { token });
            }
            return BadRequest(new { message = "Username or Password is wrong" });
        }

        private string GenerateJwtToken(int role, int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("7fb4895dcd29473f09bd3b9d1499246456dd1eda25daf3f66fd4c5bf990e257418e4d3");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, role.ToString()),
                    new Claim("userId", id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
