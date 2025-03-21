using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface ITransferRepository
{
    Transfer CreateTransfer(Transfer transfer);
}
