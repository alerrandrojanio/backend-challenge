using Challenge.Application.Services;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Challenge.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
        services.AddScoped<ITransferRepository, TransferRepository>();
    }

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(options =>
        {
            options.DefaultConnection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        });
    }

    public static IServiceCollection AddFluentValidators(this IServiceCollection services, Assembly assembly)
    {
        var validatorTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IValidator).IsAssignableFrom(t));

        foreach (var validatorType in validatorTypes)
        {
            var entityType = validatorType.BaseType?.GetGenericArguments().FirstOrDefault();
            
            if (entityType != null)
            {
                var serviceType = typeof(IValidator<>).MakeGenericType(entityType);
                
                services.AddScoped(serviceType, validatorType);
            }
        }

        return services;
    }
}
