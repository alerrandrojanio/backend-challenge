using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public PersonRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Person? GetPersonById(int id)
    {
        using MySqlCommand command = new("GetPersonById", _unitOfWork.Connection)
        {
            CommandType = CommandType.StoredProcedure,
            Transaction = _unitOfWork.Transaction 
        };

        command.Parameters.AddWithValue("@personId", id);

        using MySqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Person
            {
                Id = Guid.Parse(reader.GetString("Id")),
                Name = reader.GetString("Name")
            };
        }

        return null;
    }

    public Person CreatePerson(Person person)
    {
        using MySqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreatePerson";

        person.Id = Guid.NewGuid();

        command.Parameters.AddWithValue("@personId", person.Id);
        command.Parameters.AddWithValue("@name", person.Name);

        command.ExecuteNonQuery();

        return person;
    }
}
