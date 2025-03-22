using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IUserRepository
{
    User CreateUser(User user);

    User? GetUserById(Guid userId);
}
