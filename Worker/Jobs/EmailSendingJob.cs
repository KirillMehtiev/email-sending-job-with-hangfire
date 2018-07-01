using System;
using System.Linq;
using System.Threading.Tasks;
using Emailer.Entities;
using Emailer.Repositories;
using Hangfire;

namespace Emailer.Worker.Jobs
{
    public class EmailSendingJob : IJob
    {
        private readonly IEmailMessageRepository _emailMessageRepository;

        public EmailSendingJob(IEmailMessageRepository emailMessageRepository)
        {
            _emailMessageRepository = emailMessageRepository;
        }

        public async Task Execute()
        {
            await StartSendingEmails();
        }

        public async Task StartSendingEmails()
        {
            var emailsToSent = await _emailMessageRepository.GetAllWithStatus(EmailMessageStatus.Pending);

            foreach (var emailMessage in emailsToSent.ToList())
            {
                emailMessage.Status = EmailMessageStatus.Send;
                await _emailMessageRepository.UpdateAsync(emailMessage);
            }
        }
    }
}