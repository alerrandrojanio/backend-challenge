using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces;

namespace Challenge.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool AccountExists(string accountNumber)
    {
        throw new NotImplementedException();
    }

    public Account CreateAccount(Account account)
    {
        throw new NotImplementedException();
    }
}
