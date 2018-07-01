using System.Threading.Tasks;

namespace Emailer.Services
{
    public interface IEmailSenderService
    {
        Task Send(string from, string to, string subject, string body);
    }
}