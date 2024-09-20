using Sulpak.TestAssigment.Domain.Entities;

namespace Sulpak.TestAssigment.Application.Interfaces.Repositories;

public interface IPriceRepository
{
    Task<Price> GetPriceByArticleAndDepartmentAsync(int article, int departmentId);
    Task<List<Price>> GetPricesByDepartmentAndPriorityAsync(int departmentId, int priority);
    Task UpdatePricesAsync(List<Price> prices, int priority);
    Task<(IEnumerable<Price> prices, int totalRecords)> GetPricesPagedAsync(int pageNumber, int pageSize);
}
