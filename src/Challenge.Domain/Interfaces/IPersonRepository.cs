using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IPersonRepository
{
    Person? GetPersonById(Guid personId);

    Person CreatePerson(Person person);
}
