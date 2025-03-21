using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Account? GetAccountByAccountNumber(string accountNumber)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetAccountByAccountNumber";

        command.Parameters.Add(new SqlParameter("@accountNumber", accountNumber));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Account
            {
                Id = reader.GetGuid("AccountId"),
                AccountNumber = reader.GetString("AccountNumber"),
                Balance = reader.GetDecimal("Balance"),
                Person = new()
                {
                    Id = reader.GetGuid("PersonId")
                }
            };
        }

        return null;
    }

    public Account? GetAccountByPersonId(Guid personId)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetAccountByPersonId";

        command.Parameters.Add(new SqlParameter("@personId", personId));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Account
            {
                Id = reader.GetGuid("AccountId"),
                AccountNumber = reader.GetString("AccountNumber"),
                Balance = reader.GetDecimal("Balance"),
                Person = new()
                {
                    Id = reader.GetGuid("PersonId")
                }
            };
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
