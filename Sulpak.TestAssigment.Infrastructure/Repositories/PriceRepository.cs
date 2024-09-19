using Microsoft.EntityFrameworkCore;
using Sulpak.TestAssigment.Application.Interfaces.Repositories;
using Sulpak.TestAssigment.Domain.Entities;
using Sulpak.TestAssigment.Infrastructure.Data;

namespace Sulpak.TestAssigment.Infrastructure.Repositories;

public class PriceRepository : IPriceRepository
{
    private readonly DataContext _context;

    public PriceRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Price?> GetPriceBySkuAndDepartment(string article, string department)
    {
        return await _context.Prices
            .Where(p => p.Article == article && (p.Departments.Contains(department) || p.Departments.Count == 0))
            .OrderByDescending(p => p.Priority)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Price>> GetPricesByDepartmentAndPriority(string department, int priority)
    {
        return await _context.Prices
            .Where(p => p.Priority == priority && (p.Departments.Contains(department) || p.Departments.Count == 0))
            .ToListAsync();
    }

    public async Task UpdatePrices(List<Price> prices, int priority)
    {
        var existingPrices = await _context.Prices.Where(p => p.Priority == priority).ToListAsync();
        _context.Prices.RemoveRange(existingPrices);
        await _context.Prices.AddRangeAsync(prices);
        await _context.SaveChangesAsync();
    }
}