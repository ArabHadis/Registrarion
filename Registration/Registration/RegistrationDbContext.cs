using Microsoft.EntityFrameworkCore;
using Registration.Entities;

namespace Registration;

public class RegistrationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public RegistrationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity => entity.HasIndex(u => u.NationalCode).IsUnique());
    }
}