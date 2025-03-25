namespace Challenge.Domain.DTOs.Account;

public class CreateDepositDTO
{
    public decimal Value { get; set; }

    public Guid PersonId { get; set; }

    public string AccountNumber { get; set; } = string.Empty;
}
