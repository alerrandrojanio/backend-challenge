using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IUserTokenRepository
{
    UserToken CreateUserToken(UserToken token);

    UserToken? GetLatestValidTokenByUserId(Guid userId);
}
