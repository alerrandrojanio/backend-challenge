using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Challenge.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Data;
using System.Text.Json;

namespace Challenge.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly CacheSettings _cacheSettings;

    public PersonRepository(IUnitOfWork unitOfWork, IConnectionMultiplexer redisConnection, IOptions<CacheSettings> cacheSettings)
    {
        _unitOfWork = unitOfWork;
        _redisConnection = redisConnection;
        _cacheSettings = cacheSettings.Value;
    }

    public Person? GetPersonById(Guid personId)
    {
        string cacheKey = CacheExtensions.GenerateCacheKey<Person>(nameof(personId), personId.ToString());

        IDatabase cacheDatabase = _redisConnection.GetDatabase();

        string? cachedData = cacheDatabase.StringGet(cacheKey);

        if (cachedData is not null)
            return JsonSerializer.Deserialize<Person>(cachedData);

        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetPersonById";

        command.Parameters.Add(new SqlParameter("@personId", personId));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            Person person = new()
            {
                Id = reader.GetGuid("PersonId"),  
                Name = reader.GetString("Name"),
                Email = reader.GetString("Email"),
                Phone = reader.GetString("Phone")
            };

            cacheDatabase.StringSet(cacheKey, JsonSerializer.Serialize(person), TimeSpan.FromMinutes(_cacheSettings.MinutesToExpire));

            return person;
        }

        return null;
    }

    public Person CreatePerson(Person person)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreatePerson";

        command.Parameters.Add(new SqlParameter("@name", person.Name));
        command.Parameters.Add(new SqlParameter("@email", person.Email));
        command.Parameters.Add(new SqlParameter("@phone", person.Phone));

       var result = command.ExecuteScalar();

        if (result is not null)
            person.Id = Guid.Parse(result.ToString()!);

        return person;
    }
}