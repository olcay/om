using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Otomatik.Library.Web.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var sendGridKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");

            if (!string.IsNullOrWhiteSpace(sendGridKey))
            {
                return Execute(sendGridKey, subject, message, email);
            }

            throw new ArgumentNullException();
        }

        public Task Execute(string apiKey, string subject, string content, string email)
        {
            var client = new SendGridClient(apiKey);
            var message = new SendGridMessage()
            {
                From = new EmailAddress("otomatikmuhendisnoreply@gmail.com", "OM Library"),
                Subject = subject,
                PlainTextContent = content,
                HtmlContent = content
            };
            message.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(message);
        }
    }
}
