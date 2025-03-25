namespace Challenge.Domain.DTOs.Account.Response;

public class CreateTransferResponseDTO
{
    public Guid TransferId { get; set; }
    
    public decimal Value { get; set; }
    
    public Guid PayerId { get; set; }
    
    public Guid PayeeId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}
