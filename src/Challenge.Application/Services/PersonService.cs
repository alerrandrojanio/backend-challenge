using Challenge.Domain.DTOs;
using Challenge.Domain.DTOs.Response;
using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;
using Mapster;

namespace Challenge.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IIndividualPersonRepository _individualPersonRepository;
    private readonly IMerchantPersonRepository _merchantPersonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PersonService(IPersonRepository personRepository, IIndividualPersonRepository individualPersonRepository, IMerchantPersonRepository merchantPersonRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _individualPersonRepository = individualPersonRepository;
        _merchantPersonRepository = merchantPersonRepository;
        _unitOfWork = unitOfWork;
    }

    public CreateIndividualPersonResponseDTO? CreateIndividualPerson(CreateIndividualPersonDTO createIndividualPersonDTO)
    {
        CreateIndividualPersonResponseDTO createIndividualPersonResponseDTO = null!;
        
        try
        {
            _unitOfWork.BeginTransaction();

            Person person = createIndividualPersonDTO.Adapt<Person>();

            person = _personRepository.CreatePerson(person);

            IndividualPerson individualPerson = ValueTuple.Create(createIndividualPersonDTO, person).Adapt<IndividualPerson>();

            _individualPersonRepository.CreateIndividualPerson(individualPerson);

            _unitOfWork.Commit();

            createIndividualPersonResponseDTO = individualPerson.Adapt<CreateIndividualPersonResponseDTO>();
        }
        catch
        {
            _unitOfWork.Rollback();
        }

        return createIndividualPersonResponseDTO;
    }

    public CreateMerchantPersonResponseDTO? CreateMerchantPerson(CreateMerchantPersonDTO createMerchantPersonDTO)
    {
        CreateMerchantPersonResponseDTO createMerchantPersonResponseDTO = null!;

        try
        {
            _unitOfWork.BeginTransaction();

            Person person = createMerchantPersonDTO.Adapt<Person>();

            person = _personRepository.CreatePerson(person);

            MerchantPerson merchantPerson = ValueTuple.Create(createMerchantPersonDTO, person).Adapt<MerchantPerson>();

            _merchantPersonRepository.CreateMerchantPerson(merchantPerson);

            _unitOfWork.Commit();

            createMerchantPersonResponseDTO = merchantPerson.Adapt<CreateMerchantPersonResponseDTO>();
        }
        catch
        {
            _unitOfWork.Rollback();
        }

        return createMerchantPersonResponseDTO;
    }
}
