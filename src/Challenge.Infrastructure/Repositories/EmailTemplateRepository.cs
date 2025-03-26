using Challenge.Domain.Entities;
using Challenge.Domain.Enums;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Challenge.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Data;
using System.Text.Json;

namespace Challenge.Infrastructure.Repositories;

public class EmailTemplateRepository : IEmailTemplateRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly CacheSettings _cacheSettings;

    public EmailTemplateRepository(IUnitOfWork unitOfWork, IConnectionMultiplexer redisConnection, IOptions<CacheSettings> cacheSettings)
    {
        _unitOfWork = unitOfWork;
        _redisConnection = redisConnection;
        _cacheSettings = cacheSettings.Value;
    }

    public EmailTemplate? GetEmailTemplateByType(EmailType emailType)
    {
        string cacheKey = CacheExtensions.GenerateCacheKey<EmailTemplate>(nameof(emailType), emailType.ToString());

        IDatabase cacheDatabase = _redisConnection.GetDatabase();

        string? cachedData = cacheDatabase.StringGet(cacheKey);

        if (cachedData is not null)
            return JsonSerializer.Deserialize<EmailTemplate>(cachedData);

        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetEmailTemplateByType";

        command.Parameters.Add(new SqlParameter("@emailType", emailType.ToString()));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            EmailTemplate emailTemplate = new()
            {
                Id = reader.GetGuid("EmailTemplateId"),
                Subject = reader.GetString("Subject"),
                Body = reader.GetString("Body"),
                Type = Enum.Parse<EmailType>(reader.GetString("Type"))
            };

            cacheDatabase.StringSet(cacheKey, JsonSerializer.Serialize(emailTemplate), TimeSpan.FromMinutes(_cacheSettings.MinutesToExpire));

            return emailTemplate;
        }

        return null;
    }
}
