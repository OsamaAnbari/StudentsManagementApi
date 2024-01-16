using NuGet.Protocol.Plugins;
using Org.BouncyCastle.Crypto.Generators;
using Students_Management_Api.Models;

namespace Students_Management_Api.Services
{
    public class AuthService
    {
        public async Task<User?> Login(Login model, LibraryContext context)
        {
            User user = context.User.First(u => u.Username == model.Username);

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    return user;
                }
            }
            return null;
        }
    }
}
