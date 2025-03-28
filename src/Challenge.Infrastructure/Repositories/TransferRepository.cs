using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class TransferRepository : ITransferRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public TransferRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Transfer CreateTransfer(Transfer transfer)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreateTransfer";

        command.Parameters.Add(new SqlParameter("@payerId", transfer.PayerId));
        command.Parameters.Add(new SqlParameter("@payeeId", transfer.PayeeId));
        command.Parameters.Add(new SqlParameter("@value", transfer.Value));
        command.Parameters.Add(new SqlParameter("@createdAt", transfer.CreatedAt = DateTime.Now));

        var result = command.ExecuteScalar();

        if (result is not null)
            transfer.Id = Guid.Parse(result.ToString()!);

        return transfer;
    }
}
