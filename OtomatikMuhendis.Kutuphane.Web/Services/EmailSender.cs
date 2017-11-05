using Microsoft.Extensions.Options;
using OtomatikMuhendis.Kutuphane.Web.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace OtomatikMuhendis.Kutuphane.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly WebsiteOptions _options;

        public EmailSender(IOptions<WebsiteOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }
        
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var sendGridKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");

            if (!string.IsNullOrWhiteSpace(sendGridKey))
            {
                return Execute(sendGridKey, subject, message, email);
            }

            throw new ArgumentNullException();
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_options.MailFrom, _options.Name),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}