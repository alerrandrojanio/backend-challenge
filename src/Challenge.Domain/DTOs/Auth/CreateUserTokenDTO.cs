namespace Challenge.Domain.DTOs.Auth;

public class CreateUserTokenDTO
{
    public Guid UserId { get; set; }

    public string Password { get; set; } = string.Empty;
}
