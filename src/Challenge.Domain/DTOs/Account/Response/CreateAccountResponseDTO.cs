namespace Challenge.Domain.DTOs.Account.Response;

public class CreateAccountResponseDTO
{
    public string PersonId { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}
