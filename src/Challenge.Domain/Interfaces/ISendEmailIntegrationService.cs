using Challenge.Domain.DTOs.Email;

namespace Challenge.Domain.Interfaces;

public interface ISendEmailIntegrationService
{
    Task SendEmailAsync(EmailDTO emailDTO);
}
