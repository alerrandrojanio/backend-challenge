using Challenge.API.Models.User;
using Challenge.Domain.DTOs.User;
using Challenge.Domain.DTOs.User.Response;
using Challenge.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUserModel createUserModel)
    {
        CreateUserDTO createUserDTO = createUserModel.Adapt<CreateUserDTO>();

        CreateUserResponseDTO? createUserResponseDTO = _userService.CreateUser(createUserDTO);

        if (createUserResponseDTO is null)
            return BadRequest();

        return Ok(createUserResponseDTO);
    }
}
