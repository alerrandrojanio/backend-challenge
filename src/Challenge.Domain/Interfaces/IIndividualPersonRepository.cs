using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IIndividualPersonRepository
{
    void CreateIndividualPerson(IndividualPerson individualPerson);
}
