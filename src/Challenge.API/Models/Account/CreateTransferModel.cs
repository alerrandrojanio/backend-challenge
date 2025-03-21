namespace Challenge.API.Models.Account;

public class CreateTransferModel
{
    public decimal Value { get; set; }

    public string PayerId { get; set; } = string.Empty;

    public string PayeeId { get; set; } = string.Empty;
}
