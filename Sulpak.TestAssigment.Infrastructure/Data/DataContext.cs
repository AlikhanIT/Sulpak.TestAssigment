using Microsoft.EntityFrameworkCore;
using Sulpak.TestAssigment.Domain.Entities;

namespace Sulpak.TestAssigment.Infrastructure.Data;

public class DataContext : DbContext
{
    public DbSet<Price> Prices { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Price>()
            .Property(p => p.Departments)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}
