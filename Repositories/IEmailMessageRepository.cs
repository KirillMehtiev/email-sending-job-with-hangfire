using System.Collections.Generic;
using System.Threading.Tasks;
using Emailer.Entities;

namespace Emailer.Repositories
{
    public interface IEmailMessageRepository
    {
        Task<EmailMessage> GetByIdAsync(int id);
        Task<int> InsertAsync(EmailMessage emailMessage);
        Task<bool> UpdateAsync(EmailMessage emailMessage);
        Task<bool> DeleteAsync(EmailMessage emailMessage);

        Task<IEnumerable<EmailMessage>> GetAllWithStatus(EmailMessageStatus status);
    }
}