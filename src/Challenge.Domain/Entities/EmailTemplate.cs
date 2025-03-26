using Challenge.Domain.Entities.Base;
using Challenge.Domain.Enums;

namespace Challenge.Domain.Entities;

public class EmailTemplate : BaseEntity
{
    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public EmailType Type { get; set; }
}
