namespace Challenge.Domain.DTOs.Auth.Response;

public class CreateTokenResponseDTO
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }
}
