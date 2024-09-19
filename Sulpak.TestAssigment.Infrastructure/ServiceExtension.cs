using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sulpak.TestAssigment.Application.Interfaces.Repositories;
using Sulpak.TestAssigment.Application.UseCases;
using Sulpak.TestAssigment.Infrastructure.Data;
using Sulpak.TestAssigment.Infrastructure.Repositories;

namespace Sulpak.TestAssigment.Infrastructure;

public static class ServiceExtension
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Подключение к базе данных SQLite
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlite("Data Source=prices.db"));

        // Другие сервисы
        builder.Services.AddScoped<IPriceRepository, PriceRepository>();
        builder.Services.AddScoped<PriceUseCase>();
    }

}