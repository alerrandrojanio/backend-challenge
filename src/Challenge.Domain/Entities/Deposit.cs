using Challenge.Domain.Entities.Base;

namespace Challenge.Domain.Entities;

public class Deposit : BaseEntity
{
    public decimal Value { get; set; }
    
    public Person? Person { get; set; }
    
    public string AccountNumber { get; set; } = string.Empty;
}
