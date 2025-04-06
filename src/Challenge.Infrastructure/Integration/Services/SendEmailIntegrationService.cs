using Challenge.Domain.DTOs.Email;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Challenge.Domain.DTOs.Logging;
using Mapster;

namespace Challenge.Infrastructure.Integration.Services;

public class SendEmailIntegrationService : ISendEmailIntegrationService
{
    private readonly EmailSettings _emailSettings;
    private readonly IMongoDbLogger _mongoDbLogger;

    public SendEmailIntegrationService(IOptions<EmailSettings> emailSettings, IMongoDbLogger mongoDbLogger)
    {
        _emailSettings = emailSettings.Value;
        _mongoDbLogger = mongoDbLogger;
    }

    public async Task SendEmailAsync(EmailDTO emailDTO)
    {
        try
        {
            MimeMessage message = new();
            
            message.From.Add(new MailboxAddress(_emailSettings.SenderEmail, _emailSettings.SenderEmail));
            
            message.To.Add(new MailboxAddress(emailDTO.ReceiverName, emailDTO.EmailTo));
            
            message.Subject = emailDTO.Subject;
            
            message.Body = new TextPart("html")
            {
                Text = emailDTO.Body
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port);

                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                
                await client.SendAsync(message);
                
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogDTO errorLogDTO = ex.Adapt<ErrorLogDTO>();

            await _mongoDbLogger.RegisterLog(errorLogDTO);
        }
    }
}
