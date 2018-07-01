﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Emailer.Entities;
using Emailer.Repositories;
using Hangfire;

namespace Emailer.Worker.Jobs
{
    public class EmailSendigJob : IEmailSendingJob
    {
        private readonly IEmailMessageRepository _emailMessageRepository;

        public EmailSendigJob(IEmailMessageRepository emailMessageRepository)
        {
            _emailMessageRepository = emailMessageRepository;
        }

        public async Task Execute()
        {
            await StartSendingEmails();
        }

        public async Task StartSendingEmails()
        {
            var emailsCount = 2;
            var emailsToSent = await _emailMessageRepository.GetEmailsForSending(emailsCount);

            foreach (var emailMessage in emailsToSent.ToList())
            {
                emailMessage.Status = EmailMessageStatus.Send;
                await _emailMessageRepository.UpdateAsync(emailMessage);
            }
        }
    }
}