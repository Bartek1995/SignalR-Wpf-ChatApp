using Microsoft.EntityFrameworkCore;
using Models;


public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}