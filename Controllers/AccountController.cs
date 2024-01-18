using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Students_Management_Api.Models;
using Students_Management_Api.Services;

namespace Students_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager, IConfiguration configuraion, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuraion;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    AuthService userService = new AuthService(_configuration);
                    var token = userService.GenerateJwtToken(0, 0);

                    _logger.LogInformation("Admin Logged In");
                    return Ok(new { token });
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(new { message = "Username or Password is wrong" });
            }

            return BadRequest(new { message = "Username or Password is wrong" });;
        }
    }
}
