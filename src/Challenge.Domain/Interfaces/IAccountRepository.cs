using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IAccountRepository
{
    Account? GetAccountByAccountNumber(string accountNumber);

    Account? GetAccountByPersonId(Guid personId);

    Account CreateAccount(Account account);

    void UpdateAccountBalance(Account account);
}
