using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IAccountRepository
{
    Account? GetAccountByAccountNumber(string accountNumber);

    Account CreateAccount(Account account);
}
