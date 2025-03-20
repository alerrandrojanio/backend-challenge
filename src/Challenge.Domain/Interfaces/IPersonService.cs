using Challenge.Domain.DTOs.Person;
using Challenge.Domain.DTOs.Person.Response;

namespace Challenge.Domain.Interfaces;

public interface IPersonService
{
    CreateIndividualPersonResponseDTO? CreateIndividualPerson(CreateIndividualPersonDTO createIndividualPersonDTO);

    CreateMerchantPersonResponseDTO? CreateMerchantPerson(CreateMerchantPersonDTO createMerchantPersonDTO);
}
