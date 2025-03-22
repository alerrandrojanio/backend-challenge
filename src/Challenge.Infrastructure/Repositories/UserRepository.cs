using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public UserRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetUserById";

        command.Parameters.Add(new SqlParameter("@userId", userId));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new User
            {
                Id = reader.GetGuid("UserId"),
                Name = reader.GetString("Name"),
                Email = reader.GetString("Email"),
                PasswordHash = reader.GetString("PasswordHash"),
                CreatedAt = reader.GetDateTime("CreatedAt")
            };
        }

        return null;
    }
}
