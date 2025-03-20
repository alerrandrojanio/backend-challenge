namespace Challenge.Domain.DTOs.Person.Response;

public class CreateIndividualPersonResponseDTO
{
    public Guid PersonId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string CPF { get; set; } = string.Empty;
}
