using WpfApp1.Models;
using Chat.Models;

namespace ChatServer.Services
{
    public class UserService
    {
        private CsharpdbContext _context = new();
        private readonly List<User> logins = new List<User>();
        public UserService()
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                logins.Add(new User(user.Username, user.Password));
            }
            logins.Add(new User("admin", "admin"));
        }
        public bool CheckLogin(string login, string password)
        {
            var user = logins.FirstOrDefault(l => l.login == login);
            if (user is null) return false;

            return user.password == password;
        }
    }



    public record User(string login, string password);
}
