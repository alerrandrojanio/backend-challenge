using Challenge.Domain.DTOs.Client.Base;

namespace Challenge.Domain.DTOs.Client;

public class ClientGetRequestDTO : ClientRequestDTO
{
    public Dictionary<string, string>? Headers { get; set; }

    public Dictionary<string, string>? QueryParams { get; set; }

    public string? Content { get; set; }
}
