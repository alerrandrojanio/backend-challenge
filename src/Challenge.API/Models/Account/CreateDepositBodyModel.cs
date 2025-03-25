namespace Challenge.API.Models.Account;

public class CreateDepositBodyModel
{
    public decimal Value { get; set; }

    public string PersonId { get; set; } = string.Empty;
}
