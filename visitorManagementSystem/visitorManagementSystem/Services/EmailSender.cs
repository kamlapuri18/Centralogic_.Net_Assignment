using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace visitorManagementSystem.Services
{
    public class EmailSender
    {
        public async Task SendEmail(string subject, string toEmail, string username, string message)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ksudhanshu0704@gmail.com", "Demo");
            var to = new EmailAddress(toEmail, username);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
