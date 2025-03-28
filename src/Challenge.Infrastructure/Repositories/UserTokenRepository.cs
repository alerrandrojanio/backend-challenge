using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class UserTokenRepository : IUserTokenRepository
{
    public readonly IUnitOfWork _unitOfWork;

    public UserTokenRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public UserToken CreateUserToken(UserToken userToken)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreateUserToken";

        command.Parameters.Add(new SqlParameter("@token", userToken.Token));
        command.Parameters.Add(new SqlParameter("@expiration", userToken.Expiration));
        command.Parameters.Add(new SqlParameter("@userId", userToken.User!.Id));

        var result = command.ExecuteScalar();

        if (result is not null)
            userToken.Id = Guid.Parse(result.ToString()!);

        return userToken;
    }

    public UserToken? GetLatestValidTokenByUserId(Guid userId)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetLatestValidTokenByUserId";

        command.Parameters.Add(new SqlParameter("@userId", userId));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new UserToken
            {
                Id = reader.GetGuid("UserTokenId"),
                Token = reader.GetString("Token"),
                Expiration = reader.GetDateTime("Expiration"),
                User = new()
                {
                    Id = reader.GetGuid("UserId")
                }
            };
        }

        return null;
    }
}
