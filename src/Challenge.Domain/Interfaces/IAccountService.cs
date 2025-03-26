using Challenge.Domain.DTOs.Account;
using Challenge.Domain.DTOs.Account.Response;

namespace Challenge.Domain.Interfaces;

public interface IAccountService
{
    CreateAccountResponseDTO? CreateAccount(CreateAccountDTO createAccountDTO);

    Task<CreateTransferResponseDTO?> CreateTransfer(CreateTransferDTO createTransferDTO);

    CreateDepositResponseDTO? CreateDeposit(CreateDepositDTO createDepositDTO);
}
