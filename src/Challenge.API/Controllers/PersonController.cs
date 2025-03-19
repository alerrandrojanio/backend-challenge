using Challenge.API.Models;
using Challenge.Domain.DTOs;
using Challenge.Domain.DTOs.Response;
using Challenge.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateIndividualPerson([FromBody] CreateIndividualPersonModel createIndividualPersonModel)
    {
        CreateIndividualPersonDTO createIndividualPersonDTO = createIndividualPersonModel.Adapt<CreateIndividualPersonDTO>();

        CreateIndividualPersonResponseDTO? createIndividualPersonResponseDTO = _personService.CreateIndividualPerson(createIndividualPersonDTO);

        if (createIndividualPersonResponseDTO is null)
            return BadRequest();
        
        return Ok(createIndividualPersonResponseDTO);
    }
}
