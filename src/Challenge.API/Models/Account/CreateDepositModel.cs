using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Models.Account;

public class CreateDepositModel
{
    [FromRoute]
    public string AccountNumber { get; set; } = string.Empty;

    [FromBody]
    public CreateDepositBodyModel? Body { get; set; }
}
