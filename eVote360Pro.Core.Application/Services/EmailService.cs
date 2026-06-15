using eVote360Pro.Core.Application.DTOs.Email;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace eVote360Pro.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(EmailRequestDTO emailRequestDTO)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(emailRequestDTO.To))
                {
                    emailRequestDTO.ToRange ??= new List<string>();
                    emailRequestDTO.ToRange.Add(emailRequestDTO.To);
                }

                MimeMessage email = new()
                {
                    Sender = MailboxAddress.Parse(_mailSettings.EmailFrom),
                    Subject = emailRequestDTO.Subject
                };

                foreach (var toItem in emailRequestDTO.ToRange ?? new List<string>())
                {
                    if (!string.IsNullOrWhiteSpace(toItem))
                    {
                        email.To.Add(MailboxAddress.Parse(toItem));
                    }
                }

                BodyBuilder builder = new()
                {
                    HtmlBody = emailRequestDTO.HtmlBody
                };

                email.Body = builder.ToMessageBody();

                using SmtpClient smtpClient = new();

                await smtpClient.ConnectAsync(
                    _mailSettings.SmtpHost,
                    _mailSettings.SmtpPort,
                    SecureSocketOptions.StartTls);

                await smtpClient.AuthenticateAsync(
                    _mailSettings.SmtpUser,
                    _mailSettings.SmtpPass);

                await smtpClient.SendAsync(email);

                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error enviando el correo.");
            }
        }
    }
}