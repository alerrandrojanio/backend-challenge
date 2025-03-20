using Challenge.Application.Language;
using Challenge.Domain.DTOs.Account;
using Challenge.Domain.DTOs.Account.Response;
using Challenge.Domain.Entities;
using Challenge.Domain.Exceptions;
using Challenge.Domain.Interfaces;
using Mapster;
using System.Net;

namespace Challenge.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IAccountRepository accountRepository, IPersonRepository personRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public CreateAccountResponseDTO? CreateAccount(CreateAccountDTO createAccountDTO)
    {
        CreateAccountResponseDTO createAccountResponseDTO = null!;

        try
        {
            _unitOfWork.BeginTransaction();

            Person? person = _personRepository.GetPersonById(createAccountDTO.PersonId);

            if (person is null)
                throw new ServiceException(string.Format(ResourceMsg.Person_NotFound, createAccountDTO.PersonId), HttpStatusCode.BadRequest);

            Account? accountInDatabase = _accountRepository.GetAccountByAccountNumber(createAccountDTO.AccountNumber);

            if (accountInDatabase is not null)
                throw new ServiceException(ResourceMsg.Account_Exists, HttpStatusCode.BadRequest);

            Account account = ValueTuple.Create(createAccountDTO, person).Adapt<Account>();

            account = _accountRepository.CreateAccount(account);

            createAccountResponseDTO = account.Adapt<CreateAccountResponseDTO>();

            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
        }

        return createAccountResponseDTO;
    }
}
