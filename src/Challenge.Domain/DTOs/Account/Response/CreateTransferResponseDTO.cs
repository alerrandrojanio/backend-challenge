namespace Challenge.Domain.DTOs.Account.Response;

public class CreateTransferResponseDTO
{
    public Guid TransferId { get; set; }
    
    public decimal Value { get; set; }
    
    public Guid PayerId { get; set; }
    
    public Guid PayeeId { get; set; }

    public string PayeeName { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
