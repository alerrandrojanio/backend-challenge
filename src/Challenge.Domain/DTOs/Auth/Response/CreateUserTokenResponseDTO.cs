namespace Challenge.Domain.DTOs.Auth.Response;

public class CreateUserTokenResponseDTO
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }
}
