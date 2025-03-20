using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public PersonRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Person? GetPersonById(Guid personId)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetPersonById";

        command.Parameters.Add(new SqlParameter("@personId", personId));

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Person
            {
                Id = reader.GetGuid("PersonId"),  
                Name = reader.GetString("Name"),
                Email = reader.GetString("Email"),
                Phone = reader.GetString("Phone")
            };
        }

        return null;
    }

    public Person CreatePerson(Person person)
    {
        using SqlCommand command = _unitOfWork.Connection.CreateCommand();

        command.Transaction = _unitOfWork.Transaction;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CreatePerson";

        command.Parameters.Add(new SqlParameter("@name", person.Name));
        command.Parameters.Add(new SqlParameter("@email", person.Email));
        command.Parameters.Add(new SqlParameter("@phone", person.Phone));

       var result = command.ExecuteScalar();

        if (result is not null)
            person.Id = Guid.Parse(result.ToString());

        return person;
    }
}