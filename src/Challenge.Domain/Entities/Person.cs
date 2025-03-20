using Challenge.Domain.Entities.Base;

namespace Challenge.Domain.Entities;

public class Person : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;
}
