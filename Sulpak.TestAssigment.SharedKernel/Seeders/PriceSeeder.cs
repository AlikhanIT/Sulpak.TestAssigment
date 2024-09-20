using Sulpak.TestAssigment.Domain.Entities;

namespace Sulpak.TestAssigment.SharedKernel.Seeders;

public class PriceSeeder
{
    public static IEnumerable<Price> SeedPrices(List<Product> products, List<PriceType> priceTypes)
    {
        var random = new Random();

        foreach (var product in products)
        {
            foreach (var priceType in priceTypes)
            {
                var price = new Price
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    PriceTypeId = priceType.Id,
                    Cost = random.Next(50, 500),
                    Priority = random.Next(1, 10)
                };

                var departmentCount = random.Next(0, 10);
                var departmentIds = new HashSet<int>(); 

                for (var i = 0; i < departmentCount; i++)
                    departmentIds.Add(random.Next(1, 100));

                price.Departments = departmentIds.ToArray();

                yield return price;
            }
        }
    }
}