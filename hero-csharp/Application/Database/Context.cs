using hero_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace hero_csharp.Database;

public class Context : DbContext
{
    public DbSet<Hero> Heroes { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hero>().HasKey(k => k.Id);
        modelBuilder.Entity<Hero>().Property(p => p.Name).IsRequired();
    }
}
