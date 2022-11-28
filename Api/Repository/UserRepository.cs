using System.Net;
using Models;

namespace Api.Repository;

public class UserRepository
{
    private readonly UsersDbContext _usersDbContext;

    public UserRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<HttpStatusCode> AddUser(User user)
    {
        _usersDbContext.Users.Add(user);
        await _usersDbContext.SaveChangesAsync();
        return HttpStatusCode.Created;
    }
}