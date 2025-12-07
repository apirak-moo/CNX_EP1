using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace backend.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Tool> Tools { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
