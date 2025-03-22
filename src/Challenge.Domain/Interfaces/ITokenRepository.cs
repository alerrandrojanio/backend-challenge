using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface ITokenRepository
{
    Token CreateToken(Token token);
}
