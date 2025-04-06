using System.Text.Json.Serialization;

namespace Challenge.Domain.DTOs.Transfer.Response;

public class TransferAuthorizationResponseDTO
{
    public string? Status { get; set; }

    public TransferAuthorizationDataResponseDTO? Data { get; set; }
}
