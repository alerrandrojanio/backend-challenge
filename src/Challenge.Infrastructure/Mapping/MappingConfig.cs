using Challenge.Domain.DTOs.Account;
using Challenge.Domain.DTOs.Account.Response;
using Challenge.Domain.DTOs.Person;
using Challenge.Domain.DTOs.Person.Response;
using Challenge.Domain.Entities;
using Mapster;

namespace Challenge.Infrastructure.Mapping;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        #region CreateIndividualPerson
        TypeAdapterConfig<(CreateIndividualPersonDTO createIndividualPersonDTO, Person person), IndividualPerson>.NewConfig()
           .Map(dest => dest.Person, src => src.person)
           .Map(dest => dest.CPF, src => src.createIndividualPersonDTO.CPF)
           .Map(dest => dest.BirthDate, src => src.createIndividualPersonDTO.BirthDate);

        TypeAdapterConfig<IndividualPerson, CreateIndividualPersonResponseDTO>.NewConfig()
           .Map(dest => dest.PersonId, src => src.Person.Id)
           .Map(dest => dest.Name, src => src.Person.Name)
           .Map(dest => dest.CPF, src => src.CPF);
        #endregion CreateIndividualPerson

        #region CreateMerchantPerson
        TypeAdapterConfig<(CreateMerchantPersonDTO createMerchantPersonDTO, Person person), MerchantPerson>.NewConfig()
           .Map(dest => dest.Person, src => src.person)
           .Map(dest => dest.CNPJ, src => src.createMerchantPersonDTO.CNPJ)
           .Map(dest => dest.MerchantName, src => src.createMerchantPersonDTO.MerchantName)
           .Map(dest => dest.MerchantAddress, src => src.createMerchantPersonDTO.MerchantAddress)
           .Map(dest => dest.MerchantContact, src => src.createMerchantPersonDTO.MerchantContact);

        TypeAdapterConfig<MerchantPerson, CreateMerchantPersonResponseDTO>.NewConfig()
           .Map(dest => dest.PersonId, src => src.Person.Id)
           .Map(dest => dest.Name, src => src.Person.Name)
           .Map(dest => dest.CNPJ, src => src.CNPJ)
           .Map(dest => dest.MerchantName, src => src.MerchantName);
        #endregion CreateMerchantPerson

        #region CreateAccount
        TypeAdapterConfig<(CreateAccountDTO createAccountDTO, Person person), Account>.NewConfig()
           .Map(dest => dest.Person, src => src.person)
           .Map(dest => dest.AccountNumber, src => src.createAccountDTO.AccountNumber)
           .Map(dest => dest.Balance, src => src.createAccountDTO.Balance);

        TypeAdapterConfig<Account, CreateAccountResponseDTO>.NewConfig()
           .Map(dest => dest.PersonId, src => src.Person.Id)
           .Map(dest => dest.AccountNumber, src => src.AccountNumber)
           .Map(dest => dest.Balance, src => src.Balance);
        #endregion CreateAccount
    }
}
