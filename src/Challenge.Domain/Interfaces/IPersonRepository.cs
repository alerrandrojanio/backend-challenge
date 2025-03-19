using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IPersonRepository
{
    Person? GetPersonById(int id);

    Person CreatePerson(Person person);
}
