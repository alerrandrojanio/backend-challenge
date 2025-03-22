namespace Challenge.Domain.DTOs.User.Response;

public class CreateUserResponseDTO
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
}
