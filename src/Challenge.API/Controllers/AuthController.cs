﻿using Challenge.API.Models.Auth;
using Challenge.Domain.DTOs.Auth;
using Challenge.Domain.DTOs.Auth.Response;
using Challenge.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateToken([FromBody] CreateTokenModel createTokenModel)
    {
        CreateTokenDTO createTokenDTO = createTokenModel.Adapt<CreateTokenDTO>();

        CreateTokenResponseDTO? createTokenResponseDTO = _authService.CreateToken(createTokenDTO);

        if (createTokenResponseDTO is null)
            return BadRequest();

        return Ok(createTokenResponseDTO);
    }
}
