namespace Challenge.Domain.DTOs.Person;

public class CreateMerchantPersonDTO
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string CNPJ { get; set; } = string.Empty;

    public string MerchantName { get; set; } = string.Empty;

    public string MerchantAddress { get; set; } = string.Empty;

    public string MerchantContact { get; set; } = string.Empty;
}
