using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using GymManagementSystem.IServices;
using System.Threading.Tasks;

namespace EcommerceBackend.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Gym Management System", _configuration["EmailSettings:From"]));
            emailMessage.To.Add(new MailboxAddress(to, to));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["SmtpSettings:Host"], int.Parse(_configuration["SmtpSettings:Port"]), false);
                await client.AuthenticateAsync(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
