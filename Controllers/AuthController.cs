//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Students_Management_Api.Services;
//using Students_Management_Api.Models;
//using Microsoft.Extensions.Logging.Console;

//namespace Students_Management_Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly LibraryContext _context;
//        IConfiguration _configuration;
//        ILogger _logger;

//        public AuthController(LibraryContext context, IConfiguration configuration, ILogger<AuthController> logger)
//        {
//            _context = context;
//            _configuration = configuration;
//            _logger = logger;
//        }

//        [Route("user")]
//        [HttpPost()]
//        public async Task<IActionResult> UserLogin(Login model)
//        {
//            AuthService userService = new AuthService(_configuration);
//            string token = await userService.Login(model, _context);

//            if (token != null)
//            {
//                _logger.LogInformation(1015, "User Logged In");
//                return Ok(new { token });
//            }
//            return BadRequest(new { message = "Username or Password is wrong"});
//        }

//        [Route("admin")]
//        [HttpPost()]
//        public async Task<IActionResult> AdminLogin(Login model)
//        {
//            if (model.Username == "osama" && model.Password == "123123")
//            {
//                AuthService userService = new AuthService(_configuration);
//                var token = userService.GenerateJwtToken(0, 0);
                
//                _logger.LogInformation("Admin Logged In");
//                return Ok(new { token });
//            }
//            return BadRequest(new { message = "Username or Password is wrong" });
//        }
//    }
//}
