using Microsoft.EntityFrameworkCore;
using Sulpak.TestAssigment.Domain.Entities;
using Sulpak.TestAssigment.SharedKernel.Seeders;

namespace Sulpak.TestAssigment.Infrastructure.Data;

public class DataContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<PriceType> PriceTypes { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        base.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var products = ProductSeeder.SeedProducts(100);
        var priceTypes = PriceTypeSeeder.SeedPriceTypes(10);
        var prices = PriceSeeder.SeedPrices(products, priceTypes);

        modelBuilder.Entity<Product>().HasData(products);
        modelBuilder.Entity<PriceType>().HasData(priceTypes);
        modelBuilder.Entity<Price>().HasData(prices);
    }
}