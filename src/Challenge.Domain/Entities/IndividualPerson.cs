using Challenge.Domain.Entities.Base;

namespace Challenge.Domain.Entities;

public class IndividualPerson : BaseEntity
{
    public Person? Person { get; set; }

    public string CPF { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }
}
