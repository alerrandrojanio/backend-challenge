using Challenge.Domain.Entities.Base;

namespace Challenge.Domain.Entities;

public class MerchantPerson : BaseEntity
{
    public Person? Person { get; set; }
    public string CNPJ { get; set; } = string.Empty;
    public string MerchantName { get; set; } = string.Empty;
    public string MerchantAddress { get; set; } = string.Empty;
    public string MerchantContact { get; set; } = string.Empty;
}
