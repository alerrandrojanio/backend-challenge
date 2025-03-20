using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class IndividualPersonRepository : IIndividualPersonRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public IndividualPersonRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IndividualPerson CreateIndividualPerson(IndividualPerson individualPerson)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreateIndividualPerson";

        command.Parameters.Add(new SqlParameter("@personId", individualPerson.Person.Id));
        command.Parameters.Add(new SqlParameter("@cpf", individualPerson.CPF));
        command.Parameters.Add(new SqlParameter("@birthDate", individualPerson.BirthDate));

        var result = command.ExecuteScalar();

        if (result is not null)
            individualPerson.Id = Guid.Parse(result.ToString());

        return individualPerson;
    }
}
