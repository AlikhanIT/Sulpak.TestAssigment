using Sulpak.TestAssigment.Domain.Entities;

namespace Sulpak.TestAssigment.SharedKernel.Seeders;

public class PriceTypeSeeder
{
    public static List<PriceType> SeedPriceTypes(int priceTypesCount)
    {
        var priceTypes = new List<PriceType>();

        for (var i = 1; i <= priceTypesCount; i++)
            priceTypes.Add(new PriceType
            {
                Id = i,
                Name = $"PriceType {i}"
            });

        return priceTypes;
    }
}