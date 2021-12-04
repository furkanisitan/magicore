using Microsoft.EntityFrameworkCore;

namespace Core.Tests.DataAccess.EntityFramework;

internal class InMemoryDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDB");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "C1" },
            new Category { Id = 2, Name = "C2" });

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "P1", CategoryId = 1 },
            new Product { Id = 2, Name = "P2", CategoryId = 1 },
            new Product { Id = 3, Name = "P3", CategoryId = 1 },
            new Product { Id = 4, Name = "P4", CategoryId = 2 },
            new Product { Id = 5, Name = "P5", CategoryId = 2 });

        base.OnModelCreating(modelBuilder);
    }
}