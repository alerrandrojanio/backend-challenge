namespace Challenge.Domain.DTOs.Account;

public class CreateAccountDTO
{
    public Guid PersonId { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}
