namespace ChatServer.Services
{
    public class UserService
    {
        private readonly List<User> logins;
        public UserService()
        {
            logins = new()
            {
                new User("admin", "admin"),
                new User("user1", "123"),
                new User("user2", "456"),
                new User("user3", "789")
            };
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
