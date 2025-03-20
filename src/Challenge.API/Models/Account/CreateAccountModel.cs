namespace Challenge.API.Models.Account;

public class CreateAccountModel
{
    public string PersonId { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}
