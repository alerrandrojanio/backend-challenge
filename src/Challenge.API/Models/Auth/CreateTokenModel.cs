namespace Challenge.API.Models.Auth;

public class CreateTokenModel
{
    public string UserId { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
