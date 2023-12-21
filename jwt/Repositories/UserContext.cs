using jwt.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace jwt.Repositories;

public class UserContext : DbContext
{
public UserContext(DbContextOptions<UserContext> options) : base(options)
{
}

    public DbSet<User> Users { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Phone).IsUnique(); });
    }

}    
