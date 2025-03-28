using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class DepositRepository : IDepositRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public DepositRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Deposit? CreateDeposit(Deposit deposit)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreateDeposit";

        command.Parameters.Add(new SqlParameter("@personId", deposit.Person!.Id));
        command.Parameters.Add(new SqlParameter("@accountNumber", deposit.AccountNumber));
        command.Parameters.Add(new SqlParameter("@value", deposit.Value));
        command.Parameters.Add(new SqlParameter("@createdAt", deposit.CreatedAt = DateTime.Now));
        var result = command.ExecuteScalar();

        if (result is not null)
            deposit.Id = Guid.Parse(result.ToString()!);

        return deposit;
    }
}
