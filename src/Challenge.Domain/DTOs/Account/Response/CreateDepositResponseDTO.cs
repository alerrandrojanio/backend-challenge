namespace Challenge.Domain.DTOs.Account.Response;

public class CreateDepositResponseDTO
{
    public Guid DepositId { get; set; }
    
    public decimal Value { get; set; }
    
    public Guid PersonId { get; set; }
    
    public string AccountNumber { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
