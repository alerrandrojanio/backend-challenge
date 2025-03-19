namespace Challenge.Domain.Entities;

public class IndividualPerson
{
    public Person Person { get; set; }

    public string CPF { get; set; } = string.Empty;
}
