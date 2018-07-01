using Dapper.Contrib.Extensions;

namespace Emailer.Entities
{
    [Table("dbo.EmailMessage")]
    public class EmailMessage
    {
        [Key]
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string SenderEmail { get; set; }

        public string ReceiverEmail { get; set; }

        public EmailMessageStatus Status { get; set; }
    }
}