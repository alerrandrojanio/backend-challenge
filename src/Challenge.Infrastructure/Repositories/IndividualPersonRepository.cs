using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class IndividualPersonRepository : IIndividualPersonRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public IndividualPersonRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void CreateIndividualPerson(IndividualPerson individualPerson)
    {
        using MySqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreateIndividualPerson";

        command.Parameters.AddWithValue("@personId", individualPerson.Person.Id);
        command.Parameters.AddWithValue("@name", individualPerson.Person.Name);
        command.Parameters.AddWithValue("@cpf", individualPerson.CPF);

        command.ExecuteNonQuery();
    }
}
