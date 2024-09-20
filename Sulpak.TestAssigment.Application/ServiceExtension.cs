using Microsoft.Extensions.DependencyInjection;
using Sulpak.TestAssigment.Application.Interfaces.Repositories;
using Sulpak.TestAssigment.Application.UseCases;

namespace Sulpak.TestAssigment.Infrastructure;

public static class ServiceExtension
{
    public static void ConfigureUseCases(this IServiceCollection services)
    {
        services.AddScoped<PriceUseCase>();
    }
}