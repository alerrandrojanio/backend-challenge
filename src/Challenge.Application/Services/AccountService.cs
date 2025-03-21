using Challenge.Application.Resources;
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
    private readonly ITransferRepository _transferRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IAccountRepository accountRepository, IPersonRepository personRepository, ITransferRepository transferRepository,IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _personRepository = personRepository;
        _transferRepository = transferRepository;
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

            Account personHaveAccount = _accountRepository.GetAccountByPersonId(createAccountDTO.PersonId);

            if (personHaveAccount is not null)
                throw new ServiceException(ResourceMsg.Account_Person_Exists, HttpStatusCode.BadRequest);

            Account? accountInDatabase = _accountRepository.GetAccountByAccountNumber(createAccountDTO.AccountNumber);

            if (accountInDatabase is not null)
                throw new ServiceException(ResourceMsg.Account_AccountNumber_Exists, HttpStatusCode.BadRequest);

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

    public CreateTransferResponseDTO? CreateTransfer(CreateTransferDTO createTransferDTO)
    {
        CreateTransferResponseDTO createTransferResponseDTO = null!;

        try
        {
            _unitOfWork.BeginTransaction();

            Person? payer = _personRepository.GetPersonById(createTransferDTO.PayeeId);

            if (payer is null)
                throw new ServiceException(string.Format(ResourceMsg.Person_NotFound, createTransferDTO.PayeeId), HttpStatusCode.BadRequest);

            Person? payee = _personRepository.GetPersonById(createTransferDTO.PayeeId);

            if (payee is null)
                throw new ServiceException(string.Format(ResourceMsg.Person_NotFound, createTransferDTO.PayeeId), HttpStatusCode.BadRequest);

            Account payerAccount = _accountRepository.GetAccountByPersonId(createTransferDTO.PayerId);

            if (payerAccount is null)
                throw new ServiceException(string.Format(ResourceMsg.Account_Person_Exists, createTransferDTO.PayerId), HttpStatusCode.BadRequest);

            Account payeeAccount = _accountRepository.GetAccountByPersonId(createTransferDTO.PayeeId);

            if (payeeAccount is null)
                throw new ServiceException(string.Format(ResourceMsg.Account_NotFound, createTransferDTO.PayeeId), HttpStatusCode.BadRequest);

            if (payerAccount.Balance < createTransferDTO.Value)
                throw new ServiceException(ResourceMsg.Account_InsufficientFunds, HttpStatusCode.BadRequest);

            payerAccount.Balance -= createTransferDTO.Value;

            payeeAccount.Balance += createTransferDTO.Value;

            _accountRepository.UpdateAccountBalance(payerAccount);

            _accountRepository.UpdateAccountBalance(payeeAccount);

            Transfer transfer = createTransferDTO.Adapt<Transfer>();

            _transferRepository.CreateTransfer(transfer);

            createTransferResponseDTO = transfer.Adapt<CreateTransferResponseDTO>();

            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
        }

        return createTransferResponseDTO;
    }
}
