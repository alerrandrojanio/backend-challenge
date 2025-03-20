namespace Challenge.API.Models.Person;

public class CreateIndividualPersonModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string BirthDate { get; set; } = string.Empty;
}
