using Challenge.Domain.DTOs.User.Response;
using Challenge.Domain.DTOs.User;
using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IUserService
{
    CreateUserResponseDTO? CreateUser(CreateUserDTO createUserDTO);

    User? GetUserById(Guid userId);

    void ValidateUser(Guid userId, string password);
}
