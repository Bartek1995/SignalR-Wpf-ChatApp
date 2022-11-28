using Microsoft.EntityFrameworkCore;

namespace Models;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Person> Users { get; set; }
}