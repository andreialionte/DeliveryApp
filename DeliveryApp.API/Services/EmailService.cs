using DeliveryApp.API.Helpers;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DeliveryApp.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;
        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger)
        {
            emailSettings = options.Value;
            this.logger = logger;
        }

        public async Task SendEmailAsync(Mailrequest mailrequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(emailSettings.Email);
                email.To.Add(MailboxAddress.Parse(mailrequest.ToEmail));
                email.Subject = mailrequest.Subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = mailrequest.Body
                };
                email.Body = builder.ToMessageBody();

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                logger.LogInformation("Connecting to SMTP server {Host}:{Port}", emailSettings.Host, emailSettings.Port);
                smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTlsWhenAvailable);

                logger.LogInformation("Authenticating with SMTP server using email {Email}", emailSettings.Email);
                smtp.Authenticate(emailSettings.Email, emailSettings.Password);

                logger.LogInformation("Sending email to {Email}", mailrequest.ToEmail);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                logger.LogInformation("Email sent successfully to {Email}", mailrequest.ToEmail);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending email to {Email}", mailrequest.ToEmail);
                throw;
            }
        }
    }
}
