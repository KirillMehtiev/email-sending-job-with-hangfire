using System.Threading.Tasks;
using Emailer.Entities;

namespace Emailer.Services
{
    public interface IEmailMessageService
    {
        Task<EmailMessage> GetAsync(int id);
        Task InsertAsync(EmailMessage emailMessage);
        Task UpdateStatusGetAsync(int id, EmailMessageStatus emailMessage);
    }
}