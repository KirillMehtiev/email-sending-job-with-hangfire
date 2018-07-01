using System.Threading.Tasks;

namespace Emailer.Worker.Jobs
{
    public interface IEmailSendingJob
    {
        Task Execute();
    }
}