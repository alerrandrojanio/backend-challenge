using Challenge.Domain.DTOs.User;
using Challenge.Domain.DTOs.User.Response;

namespace Challenge.Domain.Interfaces;

public interface IUserService
{
    CreateUserResponseDTO? CreateUser(CreateUserDTO createUserDTO);

    void ValidateUser(Guid userId, string password);
}
