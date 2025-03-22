using Challenge.Domain.Entities.Base;

namespace Challenge.Domain.Entities;

public class Token : BaseEntity
{
    public string UserToken { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }

    public User? User { get; set; }
}
