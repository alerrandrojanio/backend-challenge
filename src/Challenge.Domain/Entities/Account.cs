using Challenge.Domain.Entities.Base;

namespace Challenge.Domain.Entities;

public class Account : BaseEntity
{
    public Person Person { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}
