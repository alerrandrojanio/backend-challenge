using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Challenge.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Data;
using System.Text.Json;

namespace Challenge.Infrastructure.Repositories;

public class MerchantPersonRepository : IMerchantPersonRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly CacheSettings _cacheSettings;

    public MerchantPersonRepository(IUnitOfWork unitOfWork, IConnectionMultiplexer redisConnection, IOptions<CacheSettings> cacheSettings)
    {
        _unitOfWork = unitOfWork;
        _redisConnection = redisConnection;
        _cacheSettings = cacheSettings.Value;
    }

    public MerchantPerson CreateMerchantPerson(MerchantPerson merchantPerson)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreateMerchantPerson";

        command.Parameters.Add(new SqlParameter("@personId", merchantPerson.Person?.Id));
        command.Parameters.Add(new SqlParameter("@cnpj", merchantPerson.CNPJ));
        command.Parameters.Add(new SqlParameter("@merchantName", merchantPerson.MerchantName));
        command.Parameters.Add(new SqlParameter("@merchantAddress", merchantPerson.MerchantAddress));
        command.Parameters.Add(new SqlParameter("@merchantContact", merchantPerson.MerchantContact));

        var result = command.ExecuteScalar();

        if (result is not null)
            merchantPerson.Id = Guid.Parse(result.ToString()!);

        return merchantPerson;
    }

    public MerchantPerson? GetMerchantPersonByPersonId(Guid personId)
    {
        string cacheKey = CacheExtensions.GenerateCacheKey<MerchantPerson>(nameof(personId), personId.ToString());
        
        IDatabase cacheDatabase = _redisConnection.GetDatabase();

        string? cachedData = cacheDatabase.StringGet(cacheKey);

        if (cachedData is not null)
            return JsonSerializer.Deserialize<MerchantPerson>(cachedData);

        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetMerchantPersonByPersonId";

        command.Parameters.Add(new SqlParameter("@personId", personId));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            MerchantPerson merchantPerson =  new()
            {
                Id = reader.GetGuid("MerchantPersonId"),
                CNPJ = reader.GetString("CNPJ"),
                MerchantName = reader.GetString("MerchantName"),
                MerchantAddress = reader.GetString("MerchantAddress"),
                MerchantContact = reader.GetString("MerchantContact"),
                Person = new()
                {
                    Id = reader.GetGuid("PersonId")
                }
            };

            cacheDatabase.StringSet(cacheKey, JsonSerializer.Serialize(merchantPerson), TimeSpan.FromMinutes(_cacheSettings.MinutesToExpire));

            return merchantPerson;
        }

        return null;
    }
}