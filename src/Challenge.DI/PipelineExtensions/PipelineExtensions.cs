using Challenge.Application.Services;
using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Challenge.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
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
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IIndividualPersonRepository, IndividualPersonRepository>();
        services.AddScoped<IMerchantPersonRepository, MerchantPersonRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();

        services.AddScoped<PasswordHasher<User>>();
    }

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(options =>
        {
            options.DefaultConnection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        });

        services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
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
