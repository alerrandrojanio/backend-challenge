using Challenge.Domain.DTOs;
using Challenge.Domain.DTOs.Response;

namespace Challenge.Domain.Interfaces;

public interface IPersonService
{
    CreateIndividualPersonResponseDTO? CreateIndividualPerson(CreateIndividualPersonDTO createIndividualPersonDTO);
}
