using eVote360Pro.Core.Application.DTOs.Email;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace eVote360Pro.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<MailSettings> mailSettings,
            ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(EmailRequestDTO emailRequestDTO)
        {
            try
            {
                List<string> destinatarios = new();

                if (!string.IsNullOrWhiteSpace(emailRequestDTO.To))
                {
                    destinatarios.Add(emailRequestDTO.To);
                }

                if (emailRequestDTO.ToRange != null)
                {
                    destinatarios.AddRange(emailRequestDTO.ToRange);
                }

                destinatarios = destinatarios
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .ToList();

                if (!destinatarios.Any())
                    return;

                MimeMessage email = new()
                {
                    Subject = emailRequestDTO.Subject
                };

                email.From.Add(MailboxAddress.Parse(_mailSettings.EmailFrom));
                email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);

                foreach (var toItem in destinatarios)
                {
                    email.To.Add(MailboxAddress.Parse(toItem));
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
                Console.WriteLine(ex.ToString());
                _logger.LogError(ex, "Ocurrió un error enviando el correo.");
            }
        }
    }
}