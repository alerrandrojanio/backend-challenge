using Challenge.Domain.Entities;
using Challenge.Domain.Enums;

namespace Challenge.Domain.Interfaces;

public interface IEmailTemplateRepository
{
    EmailTemplate? GetEmailTemplateByType(EmailType emailType);
}
