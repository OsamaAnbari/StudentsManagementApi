using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Students_Management_Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }

    public class AuthUser : IdentityUser
    {
    }

    public class Role : IdentityRole
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}