using MailKit.Net.Smtp;
using MailKit.Security;
using MelodyFusion.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace MelodyFusion.BLL.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.GetValue<string>("MailCredentials:SenderName"), _configuration.GetValue<string>("MailCredentials:MailSender")));
            message.To.Add(new MailboxAddress("Someone", email));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = messageBody };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_configuration.GetValue<string>("MailCredentials:MailSender"), _configuration.GetValue<string>("MailCredentials:Password"));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
