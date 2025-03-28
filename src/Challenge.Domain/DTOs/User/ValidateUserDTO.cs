namespace Challenge.Domain.DTOs.User;

public class ValidateUserDTO
{
    public Guid UserId { get; set; }

    public string Password { get; set; } = string.Empty;
}
