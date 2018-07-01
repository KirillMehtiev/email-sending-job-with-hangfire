using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Emailer.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Emailer.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IEmailerConfiguration _configuration;

        public EmailSenderService(IEmailerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Send(string from, string to, string subject, string body)
        {
            var client = new SendGridClient(_configuration.EmailProviderKey);

            var emailFrom = new EmailAddress("no-replay@devemailer.com", "no-replay");
            var emailTo = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(emailFrom, emailTo, subject, body, body);

            var response = await client.SendEmailAsync(message);
        }
    }
}