using Challenge.Domain.DTOs.Auth;
using Challenge.Domain.DTOs.Auth.Response;

namespace Challenge.Domain.Interfaces;

public interface IAuthService
{
    CreateUserTokenResponseDTO? CreateUserToken(CreateUserTokenDTO createTokenDTO);
}
