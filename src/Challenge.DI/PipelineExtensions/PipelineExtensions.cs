using Challenge.Application.Services;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Challenge.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.DI.PipelineExtensions;

public static class PipelineExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IIndividualPersonRepository, IndividualPersonRepository>();
    }

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var dbSettings = new DatabaseSettings
        {
            DefaultConnection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty
        };
        services.AddSingleton(dbSettings);
    }
}
