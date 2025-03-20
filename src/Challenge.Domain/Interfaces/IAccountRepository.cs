using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IAccountRepository
{
    bool AccountExists(string accountNumber);

    Account CreateAccount(Account account);
}
