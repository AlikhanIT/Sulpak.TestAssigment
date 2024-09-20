using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sulpak.TestAssigment.Application.Interfaces.Repositories;
using Sulpak.TestAssigment.Application.UseCases;
using Sulpak.TestAssigment.Infrastructure.Data;
using Sulpak.TestAssigment.Infrastructure.Repositories;

namespace Sulpak.TestAssigment.Infrastructure;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlite("Data Source=prices.db"));

        return services;
    }
    
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPriceRepository, PriceRepository>();
        
        return services;
    }
}