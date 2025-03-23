using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User Entity
        modelBuilder.Entity<User>(entity =>
        {
            // Primary Key
            entity.HasKey(u => u.Id);

            // Required & Unique Constraint
            entity.Property(u => u.Email)
                .IsRequired();
            entity.HasIndex(u => u.Email).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
