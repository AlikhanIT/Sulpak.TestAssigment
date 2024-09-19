using Sulpak.TestAssigment.Domain.Entities;

namespace Sulpak.TestAssigment.Application.Interfaces.Repositories;

public interface IPriceRepository
{
    Task<Price?> GetPriceBySkuAndDepartment(string article, string department);
    Task<List<Price>> GetPricesByDepartmentAndPriority(string department, int priority);
    Task UpdatePrices(List<Price> prices, int priority);
}
