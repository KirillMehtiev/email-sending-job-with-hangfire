using System.Threading.Tasks;
using DotLiquid;
using Emailer.Entities;
using Emailer.Repositories;

namespace Emailer.Services
{
    public class EmailMessageService : IEmailMessageService
    {
        private readonly IEmailMessageRepository _emailMessageRepository;

        public EmailMessageService(IEmailMessageRepository emailMessageRepository)
        {
            _emailMessageRepository = emailMessageRepository;
        }

        public async Task<EmailMessage> GetAsync(int id)
        {
            return await InternalGetAsync(id);
        }

        public async Task InsertAsync(EmailMessage emailMessage)
        {
            // use dotliquid
            var subjectTemplate = Template.Parse(emailMessage.Subject);
            var bodyTemplate = Template.Parse(emailMessage.Body);

            emailMessage.Subject = subjectTemplate.Render(Hash.FromAnonymousObject(emailMessage));
            emailMessage.Body = bodyTemplate.Render(Hash.FromAnonymousObject(emailMessage));
            
            await _emailMessageRepository.InsertAsync(emailMessage);
        }

        public async Task UpdateStatusGetAsync(int id, EmailMessageStatus emailMessage)
        {
            var emailMessageToUpdate = await InternalGetAsync(id);

            if (emailMessageToUpdate != null)
            {
                emailMessageToUpdate.Status = emailMessage;
                await _emailMessageRepository.UpdateAsync(emailMessageToUpdate);
            }
        }

        public async Task<EmailMessage> InternalGetAsync(int id)
        {
            return await _emailMessageRepository.GetByIdAsync(id);
        }
    }
}