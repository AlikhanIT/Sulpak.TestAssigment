using Sulpak.TestAssigment.Application.Interfaces.Repositories;
using Sulpak.TestAssigment.Domain.Entities;

namespace Sulpak.TestAssigment.Application.UseCases;

public class PriceUseCase
{
    private readonly IPriceRepository _priceRepository;

    public PriceUseCase(IPriceRepository priceRepository)
    {
        _priceRepository = priceRepository;
    }

    public async Task<Price?> GetPriceBySkuAndDepartment(string sku, string department)
    {
        return await _priceRepository.GetPriceBySkuAndDepartment(sku, department);
    }

    public async Task<List<Price>> GetPricesByDepartmentAndPriority(string department, int priority)
    {
        return await _priceRepository.GetPricesByDepartmentAndPriority(department, priority);
    }

    public async Task UpdatePrices(List<Price> prices, int priority)
    {
        await _priceRepository.UpdatePrices(prices, priority);
    }
}
