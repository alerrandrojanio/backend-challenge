using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class MerchantPersonRepository : IMerchantPersonRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public MerchantPersonRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
}
