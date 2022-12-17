namespace WpfApp1.Models;

public partial class User
{
    public User(string username, string password, string? role)
    {
        Username = username;
        Password = password;
        Role = role;
    }

    public User(string username)
    {
        Username = username;
    }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Role { get; set; }
}
