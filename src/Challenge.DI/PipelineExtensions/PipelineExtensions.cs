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
        services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IIndividualPersonRepository, IndividualPersonRepository>();
        services.AddScoped<IMerchantPersonRepository, MerchantPersonRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
    }

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(options =>
        {
            options.DefaultConnection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        });
    }
}
