using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace Challenge.Infrastructure.Repositories;

public class TokenRepository : ITokenRepository
{
    public readonly IUnitOfWork _unitOfWork;

    public TokenRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Token CreateToken(Token token)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreateToken";

        command.Parameters.Add(new SqlParameter("@token", token.UserToken));
        command.Parameters.Add(new SqlParameter("@expiration", token.Expiration));
        command.Parameters.Add(new SqlParameter("@userId", token.User!.Id));

        var result = command.ExecuteScalar();

        if (result is not null)
            token.Id = Guid.Parse(result.ToString()!);

        return token;
    }

    public Token? GetLatestValidTokenByUserId(Guid userId)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetLatestValidTokenByUserId";

        command.Parameters.Add(new SqlParameter("@userId", userId));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Token
            {
                Id = reader.GetGuid("TokenId"),
                UserToken = reader.GetString("Token"),
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
