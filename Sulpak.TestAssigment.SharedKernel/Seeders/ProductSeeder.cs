using Sulpak.TestAssigment.Domain.Entities;

namespace Sulpak.TestAssigment.SharedKernel.Seeders;

public class ProductSeeder
{
    public static List<Product> SeedProducts(int productsCount)
    {
        var products = new List<Product>();

        for (var i = 1; i <= productsCount; i++)
            products.Add(new Product
            {
                Id = i,
                Name = $"Product {i}"
            });

        return products;
    }
}
