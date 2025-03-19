namespace Challenge.Domain.Entities;

public class MerchantPerson
{
    public Person Person { get; set; }

    public string CNPJ { get; set; } = string.Empty;

    public string BusinessName { get; set; } = string.Empty;
}
