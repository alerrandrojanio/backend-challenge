using System.Net;

namespace Challenge.Domain.DTOs.Client.Response;

public class ClientResponseDTO
{
    public string? Content { get; set; }

    public HttpStatusCode StatusCode { get; set; }
}
