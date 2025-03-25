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

public class AccountRepository : IAccountRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly CacheSettings _cacheSettings;

    public AccountRepository(IUnitOfWork unitOfWork, IConnectionMultiplexer redisConnection, IOptions<CacheSettings> cacheSettings)
    {
        _unitOfWork = unitOfWork;
        _redisConnection = redisConnection;
        _cacheSettings = cacheSettings.Value;
    }

    public Account? GetAccountByAccountNumber(string accountNumber)
    {
        string cacheKey = CacheExtensions.GenerateCacheKey<Account>(nameof(accountNumber), accountNumber);

        IDatabase cacheDatabase = _redisConnection.GetDatabase();

        string? cachedData = cacheDatabase.StringGet(cacheKey);

        if (cachedData is not null)
            return JsonSerializer.Deserialize<Account>(cachedData);

        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetAccountByAccountNumber";

        command.Parameters.Add(new SqlParameter("@accountNumber", accountNumber));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            Account account = new() 
            {
                Id = reader.GetGuid("AccountId"),
                AccountNumber = reader.GetString("AccountNumber"),
                Balance = reader.GetDecimal("Balance"),
                Person = new()
                {
                    Id = reader.GetGuid("PersonId")
                }
            };

            cacheDatabase.StringSet(cacheKey, JsonSerializer.Serialize(account), TimeSpan.FromMinutes(_cacheSettings.MinutesToExpire));

            return account;
        }

        return null;
    }

    public Account? GetAccountByPersonId(Guid personId)
    {
        string cacheKey = CacheExtensions.GenerateCacheKey<Account>(nameof(personId), personId.ToString());

        IDatabase cacheDatabase = _redisConnection.GetDatabase();

        string? cachedData = cacheDatabase.StringGet(cacheKey);

        if (cachedData is not null)
            return JsonSerializer.Deserialize<Account>(cachedData);

        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetAccountByPersonId";

        command.Parameters.Add(new SqlParameter("@personId", personId));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            Account account = new()
            {
                Id = reader.GetGuid("AccountId"),
                AccountNumber = reader.GetString("AccountNumber"),
                Balance = reader.GetDecimal("Balance"),
                Person = new()
                {
                    Id = reader.GetGuid("PersonId")
                }
            };

            cacheDatabase.StringSet(cacheKey, JsonSerializer.Serialize(account), TimeSpan.FromMinutes(_cacheSettings.MinutesToExpire));

            return account;
        }

        return null;
    }

    public Account CreateAccount(Account account)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreateAccount";

        command.Parameters.Add(new SqlParameter("@personId", account.Person?.Id));
        command.Parameters.Add(new SqlParameter("@accountNumber", account.AccountNumber));
        command.Parameters.Add(new SqlParameter("@balance", account.Balance));

        var result = command.ExecuteScalar();

        if (result is not null)
            account.Id = Guid.Parse(result.ToString()!);

        return account;
    }

    public void UpdateAccountBalance(Account account)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "UpdateAccountBalance";

        command.Parameters.Add(new SqlParameter("@accountId", account.Id));
        command.Parameters.Add(new SqlParameter("@balance", account.Balance));

        command.ExecuteScalar();
    }
}
