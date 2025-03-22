using Challenge.Domain.DTOs.Auth.Response;
using Challenge.Domain.DTOs.Auth;
using Challenge.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Challenge.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Challenge.Infrastructure.Repositories;
using Challenge.Domain.Entities;
using Challenge.Domain.Exceptions;
using System.Net;
using Mapster;

namespace Challenge.Application.Services;

public class AuthService : IAuthService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AuthSettings _authSettings;

    public AuthService(ITokenRepository tokenRepository, IUserService userService, IUnitOfWork unitOfWork ,IOptions<AuthSettings> authSettings)
    {
        _tokenRepository = tokenRepository;
        _userService = userService;
        _unitOfWork = unitOfWork;
        _authSettings = authSettings.Value;
    }

    public CreateTokenResponseDTO? CreateToken(CreateTokenDTO createTokenDTO)
    {
        CreateTokenResponseDTO? createTokenResponseDTO = null;
        
        try
        {
            _userService.ValidateUser(createTokenDTO.UserId, createTokenDTO.Password);

            Token? token = _tokenRepository.GetLatestValidTokenByUserId(createTokenDTO.UserId);

            if (token is not null)
                return createTokenResponseDTO = token.Adapt<CreateTokenResponseDTO>();

            DateTime expiration = DateTime.UtcNow.AddHours(1); 

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, createTokenDTO.UserId.ToString()), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime) 
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_authSettings.SecretKey));
            
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256); 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,  
                Issuer = _authSettings.Issuer,       
                Audience = _authSettings.Audience,   
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new();
            
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            
            string jwtToken = tokenHandler.WriteToken(securityToken);

            token = ValueTuple.Create(createTokenDTO, jwtToken, expiration).Adapt<Token>();

            _unitOfWork.BeginTransaction();

            token = _tokenRepository.CreateToken(token);

            createTokenResponseDTO = token.Adapt<CreateTokenResponseDTO>();

            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
        }

        return createTokenResponseDTO;
    }
}
