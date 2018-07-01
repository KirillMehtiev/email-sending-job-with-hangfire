namespace Emailer.Configuration
{
    public interface IEmailerConfiguration
    {
        string ConnectionString { get; set; }
        string EmailProviderKey { get; set; }
    }
}