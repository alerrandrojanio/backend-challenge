using Challenge.API.Models.Account;
using Challenge.Domain.DTOs.Account;
using Challenge.Domain.DTOs.Account.Response;
using Challenge.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAccount([FromBody] CreateAccountModel createAccountModel)
    {
        CreateAccountDTO createAccountDTO = createAccountModel.Adapt<CreateAccountDTO>();
        
        CreateAccountResponseDTO? createAccountResponseDTO = _accountService.CreateAccount(createAccountDTO);
        
        if (createAccountResponseDTO is null)
            return BadRequest();
        
        return Ok(createAccountResponseDTO);
    }

    [HttpPost("transfer")]
    public async Task<ActionResult> CreateTransfer([FromBody] CreateTransferModel createTransferModel)
    {
        CreateTransferDTO createTransferDTO = createTransferModel.Adapt<CreateTransferDTO>();

        CreateTransferResponseDTO? createTransferResponseDTO = _accountService.CreateTransfer(createTransferDTO);

        if (createTransferResponseDTO is null)
            return BadRequest();

        return Ok(createTransferResponseDTO);
    }

    [HttpPost("{AccountNumber}/deposit")]
    public async Task<ActionResult> CreateDeposit([FromRoute] CreateDepositModel createDepositModel)
    {
        CreateDepositDTO createDepositDTO = createDepositModel.Adapt<CreateDepositDTO>();

        CreateDepositResponseDTO? createDepositResponseDTO = _accountService.CreateDeposit(createDepositDTO);

        if (createDepositResponseDTO is null)
            return BadRequest();

        return Ok(createDepositResponseDTO);
    }
}
