using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IDepositRepository
{
    Deposit? CreateDeposit(Deposit deposit);
}
