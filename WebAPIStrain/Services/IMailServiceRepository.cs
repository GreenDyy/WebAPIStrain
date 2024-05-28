namespace WebAPIStrain.Services
{
    public interface IMailServiceRepository
    {
        public Task SendMailAsync(string toEmail, string subject, string message);
    }
}
