namespace Challenge.Domain.DTOs.Auth;

public class CreateTokenDTO
{
    public Guid UserId { get; set; }

    public string Password { get; set; } = string.Empty;
}
