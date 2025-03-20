namespace Challenge.Domain.DTOs.Response;

public class CreateMerchantPersonResponseDTO
{
    public Guid PersonId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string CNPJ { get; set; } = string.Empty;

    public string MerchantName { get; set; } = string.Empty;
}
