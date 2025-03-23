using Challenge.Domain.Entities.Base;

namespace Challenge.Domain.Entities;

public class UserToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }

    public User? User { get; set; }
}
