namespace Emailer.Configuration
{
    public class EmailerConfiguration : IEmailerConfiguration
    {
        public EmailerConfiguration(string connectionString, string emailProviderKey)
        {
            EmailProviderKey = emailProviderKey;
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
        public string EmailProviderKey { get; set; }
    }
}