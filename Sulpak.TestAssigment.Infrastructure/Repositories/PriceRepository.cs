using Microsoft.EntityFrameworkCore;
using Sulpak.TestAssigment.Application.Interfaces.Repositories;
using Sulpak.TestAssigment.Domain.Entities;
using Sulpak.TestAssigment.Infrastructure.Data;

namespace Sulpak.TestAssigment.Infrastructure.Repositories;

public class PriceRepository : IPriceRepository
{
    private readonly DataContext _context;
    private const int _batchSize = 1000;

    public PriceRepository(DataContext context)
    {
        _context = context;
    }


    public async Task<(IEnumerable<Price> prices, int totalRecords)> GetPricesPagedAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await _context.Prices.CountAsync();

        var prices = await _context.Prices
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.ProductId)
            .ToListAsync();

        return (prices, totalRecords);
    }

    public async Task<Price> GetPriceByArticleAndDepartmentAsync(int article, int departmentId)
    {
        return await _context.Prices
            .Include(p => p.Product)
            .Where(p => p.ProductId.Equals(article) &&
                        (p.Departments.Any(d => d.Equals(departmentId))))
            .OrderByDescending(p => p.Priority)
            .FirstOrDefaultAsync();
    }


    public async Task<List<Price>> GetPricesByDepartmentAndPriorityAsync(int departmentId, int priority)
    {
        return await _context.Prices
            .Include(p => p.Product)
            .Where(p => p.Priority.Equals(priority) &&
                        (p.Departments.Any(d => d == departmentId) || !p.Departments.Any()))
            .ToListAsync();
    }

    public async Task UpdatePricesAsync(List<Price> newPrices, int priority)
    {
        var existingPrices = await _context.Prices
            .Where(p => p.Priority == priority)
            .ToListAsync();

        var existingPriceMap = await _context.Prices.Where(x => newPrices.Select(f => f.Id).Contains(x.Id))
            .ToDictionaryAsync(ep => (ep.ProductId, ep.PriceTypeId));

        var pricesToUpdate = new List<Price>();
        var pricesToAdd = new List<Price>();

        foreach (var newPrice in newPrices)
        {
            newPrice.ValidateOrder();
            newPrice.Priority = priority;
            if (existingPriceMap.TryGetValue((newPrice.ProductId, newPrice.PriceTypeId), out var existingPrice))
            {
                existingPrice.Cost = newPrice.Cost;
                existingPrice.Departments = newPrice.Departments;
                existingPrice.PriceTypeId = newPrice.PriceTypeId;
                existingPrice.Priority = priority;
                pricesToUpdate.Add(existingPrice);
            }
            else
                pricesToAdd.Add(newPrice);

            if (pricesToAdd.Count >= _batchSize)
            {
                await _context.Prices.AddRangeAsync(pricesToAdd);
                pricesToAdd.Clear();
            }

            if (pricesToUpdate.Count >= _batchSize)
            {
                _context.Prices.UpdateRange(pricesToUpdate);
                pricesToUpdate.Clear();
            }
        }

        if (pricesToAdd.Count > 0)
            await _context.Prices.AddRangeAsync(pricesToAdd);

        if (pricesToUpdate.Count > 0)
            _context.Prices.UpdateRange(pricesToUpdate);

        var pricesToRemove = existingPrices
            .Where(ep => !newPrices.Any(np => np.ProductId == ep.ProductId && np.PriceTypeId == ep.PriceTypeId))
            .ToList();

        if (pricesToRemove.Count > 0)
            for (var i = 0; i < pricesToRemove.Count; i += _batchSize)
            {
                var batchToRemove = pricesToRemove.Skip(i).Take(_batchSize).ToList();
                _context.Prices.RemoveRange(batchToRemove);
            }

        await _context.SaveChangesAsync();
    }
}