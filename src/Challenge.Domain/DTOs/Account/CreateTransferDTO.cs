namespace Challenge.Domain.DTOs.Account;

public class CreateTransferDTO
{
    public decimal Value { get; set; }

    public Guid PayerId { get; set; }

    public Guid PayeeId { get; set; }
}
