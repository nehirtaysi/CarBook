using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.EmailInterfaces;
using UdemyCarBook.Application.Tools;

namespace UdemyCarBook.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendReservationEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_mailSettings.Server)
            {
                Port = _mailSettings.Port,
                Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailSettings.SenderEmail, _mailSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}