namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IEmailService
    {
        //Task CreateEmail(string recieverEmail, string subject, string messageBody);
        void SendEmail(string toAddress, string subject, string body);
        //pgbnqnaadkynbajc
    }
}
