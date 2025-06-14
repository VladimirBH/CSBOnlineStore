using CSBOnlineStore.DataBase;
using CSBOnlineStore.DataBase.Models;

namespace CSBOnlineStore.Services
{
    public class UserService
    {
        private static CSBContext _context;
        private readonly IConfiguration _configuration;
        public UserService(CSBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public static User AuthenticateUser(Login login) 
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Email);
            if (user == null)
            {
                return null;
            }
            if (user.Password == login.Password)
            {
                return user;
            }
            else 
            {
                return null;
            }
        }
    }
}
