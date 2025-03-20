using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IIndividualPersonRepository
{
    IndividualPerson CreateIndividualPerson(IndividualPerson individualPerson);
}
