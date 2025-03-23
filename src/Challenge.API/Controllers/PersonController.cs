using Challenge.API.Models.Person;
using Challenge.Domain.DTOs.Person;
using Challenge.Domain.DTOs.Person.Response;
using Challenge.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using static Challenge.API.Middlewares.AuthorizationMiddleware;

namespace Challenge.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/person")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost("individual")]
    public async Task<ActionResult> CreateIndividualPerson([FromBody] CreateIndividualPersonModel createIndividualPersonModel)
    {
        CreateIndividualPersonDTO createIndividualPersonDTO = createIndividualPersonModel.Adapt<CreateIndividualPersonDTO>();

        CreateIndividualPersonResponseDTO? createIndividualPersonResponseDTO = _personService.CreateIndividualPerson(createIndividualPersonDTO);

        if (createIndividualPersonResponseDTO is null)
            return BadRequest();
        
        return Ok(createIndividualPersonResponseDTO);
    }

    [HttpPost("merchant")]
    public async Task<ActionResult> CreateMerchantPerson([FromBody] CreateMerchantPersonModel createMerchantPersonModel)
    {
        CreateMerchantPersonDTO createMerchantPersonDTO = createMerchantPersonModel.Adapt<CreateMerchantPersonDTO>();

        CreateMerchantPersonResponseDTO? createMerchantPersonResponseDTO = _personService.CreateMerchantPerson(createMerchantPersonDTO);

        if (createMerchantPersonResponseDTO is null)
            return BadRequest();

        return Ok(createMerchantPersonResponseDTO);
    }
}
