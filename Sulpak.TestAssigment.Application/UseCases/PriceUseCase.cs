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

    public async Task<(IEnumerable<Price> prices, int totalRecords)> GetPricesPagedAsync(int pageNumber, int pageSize)
        => await _priceRepository.GetPricesPagedAsync(pageNumber, pageSize);

    public async Task<Price> GetPriceByArticleAndDepartmentAsync(int article, int departmentId)
        => await _priceRepository.GetPriceByArticleAndDepartmentAsync(article, departmentId);

    public async Task<List<Price>> GetPricesByDepartmentAndPriorityAsync(int departmentId, int priority)
        => await _priceRepository.GetPricesByDepartmentAndPriorityAsync(departmentId, priority);

    public async Task UpdatePricesAsync(List<Price> prices, int priority)
        => await _priceRepository.UpdatePricesAsync(prices, priority);
}