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

public class UserRepository : IUserRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly CacheSettings _cacheSettings;

    public UserRepository(IUnitOfWork unitOfWork, IConnectionMultiplexer redisConnection, IOptions<CacheSettings> cacheSettings)
    {
        _unitOfWork = unitOfWork;
        _redisConnection = redisConnection;
        _cacheSettings = cacheSettings.Value;
    }

    public User CreateUser(User user)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreateUser";

        command.Parameters.Add(new SqlParameter("@name", user.Name));
        command.Parameters.Add(new SqlParameter("@email", user.Email));
        command.Parameters.Add(new SqlParameter("@passwordHash", user.PasswordHash));
        
        var result = command.ExecuteScalar();

        if (result is not null)
            user.Id = Guid.Parse(result.ToString()!);

        return user;
    }

    public User? GetUserById(Guid userId)
    {
        string cacheKey = CacheExtensions.GenerateCacheKey<User>(nameof(userId), userId.ToString());

        IDatabase cacheDatabase = _redisConnection.GetDatabase();

        string? cachedData = cacheDatabase.StringGet(cacheKey);

        if (cachedData is not null)
            return JsonSerializer.Deserialize<User>(cachedData);

        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetUserById";

        command.Parameters.Add(new SqlParameter("@userId", userId));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            User user = new()
            {
                Id = reader.GetGuid("UserId"),
                Name = reader.GetString("Name"),
                Email = reader.GetString("Email"),
                PasswordHash = reader.GetString("PasswordHash"),
                CreatedAt = reader.GetDateTime("CreatedAt")
            };

            cacheDatabase.StringSet(cacheKey, JsonSerializer.Serialize(user), TimeSpan.FromMinutes(_cacheSettings.MinutesToExpire));

            return user;
        }

        return null;
    }
}
