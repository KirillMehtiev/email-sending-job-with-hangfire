using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Emailer.Configuration;
using Emailer.Entities;

namespace Emailer.Repositories
{
    public class EmailMessageRepository : IEmailMessageRepository
    {
        private readonly IEmailerConfiguration _configuration;

        public EmailMessageRepository(IEmailerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<EmailMessage> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                return await connection.GetAsync<EmailMessage>(id);
            }
        }

        public async Task<int> InsertAsync(EmailMessage emailMessage)
        {
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                return await connection.InsertAsync(emailMessage);
            }
        }

        public async Task<bool> UpdateAsync(EmailMessage emailMessage)
        {
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                return await connection.UpdateAsync(emailMessage);
            }
        }

        public async Task<bool> DeleteAsync(EmailMessage emailMessage)
        {
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                return await connection.DeleteAsync(emailMessage);
            }
        }

        public async Task<IEnumerable<EmailMessage>> GetEmailsForSending(int count)
        {
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                var pendingStatus = EmailMessageStatus.Pending;
                var sendingStatus = EmailMessageStatus.Sending;
                var query = "UPDATE TOP(@count) [dbo].[EmailMessage] SET Status = @sendingStatus OUTPUT inserted.* WHERE Status = @pendingStatus";

                using (var reader = connection.QueryMultiple(query, new { pendingStatus, count, sendingStatus }))
                {
                    return await reader.ReadAsync<EmailMessage>();
                }
            }
        }

    }
}