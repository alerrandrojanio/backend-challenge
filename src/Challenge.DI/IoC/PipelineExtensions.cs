using Challenge.Application.Services;
using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Challenge.Infrastructure.Integration.Client;
using Challenge.Infrastructure.Integration.Services;
using Challenge.Infrastructure.Logging;
using Challenge.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Reflection;

namespace Challenge.DI.IoC;

public static class PipelineExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ISendEmailIntegrationService, SendEmailIntegrationService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IIndividualPersonRepository, IndividualPersonRepository>();
        services.AddScoped<IMerchantPersonRepository, MerchantPersonRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        services.AddScoped<IDepositRepository, DepositRepository>();
        services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddScoped<ITransferAuthorizeIntegrationService, TransferAuthorizeIntegrationService>();

        services.AddScoped<IMongoDbLogger, MongoDbLogger>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddScoped<IHttpClientService, HttpClientService>();
        services.AddHttpClient<HttpClientService>();
    }

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(options =>
        {
            options.DefaultConnection = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        });

        services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
        services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
        services.Configure<TransferSettings>(configuration.GetSection(nameof(TransferSettings)));
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

    public static void AddRedisConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(provider =>
        {
            var cacheSettings = provider.GetRequiredService<IOptions<CacheSettings>>().Value;
            
            if (string.IsNullOrEmpty(cacheSettings.RedisUrl))
                throw new ArgumentNullException(nameof(cacheSettings.RedisUrl), "A conexão do Redis não foi configurada");
            
            return ConnectionMultiplexer.Connect(cacheSettings.RedisUrl);
        });
    }
}
