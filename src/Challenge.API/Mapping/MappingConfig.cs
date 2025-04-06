using Challenge.API.Models.Account;
using Challenge.Domain.DTOs.Account;
using Challenge.Domain.DTOs.Account.Response;
using Challenge.Domain.DTOs.Auth;
using Challenge.Domain.DTOs.Auth.Response;
using Challenge.Domain.DTOs.Email;
using Challenge.Domain.DTOs.Logging;
using Challenge.Domain.DTOs.Person;
using Challenge.Domain.DTOs.Person.Response;
using Challenge.Domain.DTOs.User;
using Challenge.Domain.DTOs.User.Response;
using Challenge.Domain.Entities;
using Mapster;

namespace Challenge.API.Mapping;

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
           .Map(dest => dest.PersonId, src => src.Person!.Id)
           .Map(dest => dest.Name, src => src.Person!.Name)
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
           .Map(dest => dest.PersonId, src => src.Person!.Id)
           .Map(dest => dest.Name, src => src.Person!.Name)
           .Map(dest => dest.CNPJ, src => src.CNPJ)
           .Map(dest => dest.MerchantName, src => src.MerchantName);
        #endregion CreateMerchantPerson

        #region CreateAccount
        TypeAdapterConfig<(CreateAccountDTO createAccountDTO, Person person), Account>.NewConfig()
           .Map(dest => dest.Person, src => src.person)
           .Map(dest => dest.AccountNumber, src => src.createAccountDTO.AccountNumber)
           .Map(dest => dest.Balance, src => src.createAccountDTO.Balance);

        TypeAdapterConfig<Account, CreateAccountResponseDTO>.NewConfig()
           .Map(dest => dest.PersonId, src => src.Person!.Id)
           .Map(dest => dest.AccountNumber, src => src.AccountNumber)
           .Map(dest => dest.Balance, src => src.Balance);
        #endregion CreateAccount

        #region CreateTransfer
        TypeAdapterConfig<(Transfer transfer, Person payee, Account payeeAccount), CreateTransferResponseDTO>.NewConfig()
           .Map(dest => dest.TransferId, src => src.transfer.Id)
           .Map(dest => dest.Value, src => src.transfer.Value)
           .Map(dest => dest.PayerId, src => src.transfer.PayerId)
           .Map(dest => dest.PayeeId, src => src.transfer.PayeeId)
           .Map(dest => dest.AccountNumber, src => src.payeeAccount.AccountNumber)
           .Map(dest => dest.CreatedAt, src => src.transfer.CreatedAt)
           .Map(dest => dest.PayeeName, src => src.payee.Name);
        #endregion CreateTransfer

        #region CreateUser
        TypeAdapterConfig<(CreateUserDTO createUserDTO, string passwordHash), User>.NewConfig()
           .Map(dest => dest.Name, src => src.createUserDTO.Name)
           .Map(dest => dest.Email, src => src.createUserDTO.Email)
           .Map(dest => dest.PasswordHash, src => src.passwordHash);

        TypeAdapterConfig<User, CreateUserResponseDTO>.NewConfig()
           .Map(dest => dest.UserId, src => src.Id)
           .Map(dest => dest.Name, src => src.Name);
        #endregion CreateUser

        #region CreateToken
        TypeAdapterConfig<(CreateUserTokenDTO createTokenDTO, string token, DateTime expiration), UserToken>.NewConfig()
           .Map(dest => dest.Token, src => src.token)
           .Map(dest => dest.Expiration, src => src.expiration)
           .Map(dest => dest.User!.Id, src => src.createTokenDTO.UserId);

        TypeAdapterConfig<UserToken, CreateUserTokenResponseDTO>.NewConfig()
           .Map(dest => dest.Token, src => src.Token)
           .Map(dest => dest.Expiration, src => src.Expiration);
        #endregion CreateToken

        #region CreateDeposit
        TypeAdapterConfig<CreateDepositModel, CreateDepositDTO>.NewConfig()
           .Map(dest => dest.Value, src => src.Body!.Value)
           .Map(dest => dest.PersonId, src => src.Body!.PersonId)
           .Map(dest => dest.AccountNumber, src => src.AccountNumber);

        TypeAdapterConfig<CreateDepositDTO, Deposit>.NewConfig()
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.AccountNumber, src => src.AccountNumber)
            .Map(dest => dest.Person!.Id, src => src.PersonId);

        TypeAdapterConfig<Deposit, CreateDepositResponseDTO>.NewConfig()
            .Map(dest => dest.DepositId, src => src.Id)
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.PersonId, src => src.Person!.Id)
            .Map(dest => dest.AccountNumber, src => src.AccountNumber)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);
        #endregion CreateDeposit

        #region EmailTemplate
        TypeAdapterConfig<(Person payee, EmailTemplate emailTemplate), EmailDTO>.NewConfig()
            .Map(dest => dest.ReceiverName, src => src.payee.Name)
            .Map(dest => dest.EmailTo, src => src.payee.Email)
            .Map(dest => dest.Subject, src => src.emailTemplate.Subject)
            .Map(dest => dest.Body, src => src.emailTemplate.Body);
        #endregion EmailTemplate

        #region ErrorLog
        TypeAdapterConfig<Exception, ErrorLogDTO>.NewConfig()
            .Map(dest => dest.Message, src => src.Message)
            .Map(dest => dest.StackTrace, src => src.StackTrace)
            .Map(dest => dest.Source, src => src.Source)
            .Map(dest => dest.InnerException, src => src.InnerException);
        #endregion ErrorLog
    }
}
