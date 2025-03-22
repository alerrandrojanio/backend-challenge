using Challenge.Domain.DTOs.User.Response;
using Challenge.Domain.DTOs.User;

namespace Challenge.Domain.Interfaces;

public interface IUserService
{
    CreateUserResponseDTO? CreateUser(CreateUserDTO createUserDTO);

    void ValidateUser(Guid userId, string password);
}
