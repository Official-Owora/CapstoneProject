using CapstoneProject.Application.Services.Abstractions;
using System.Net.Mail;
using System.Net;

namespace CapstoneProject.Application.Services.Implementations
{
    public class EmailService : IEmailService
    {
        
        public async Task CreateEmail(string recieverEmail, string subject, string messageBody)
        {
            var senderEmail = "owora.chukwuemeka@gmail.com";
            var CapstoneProject = "pgbnqnaadkynbajc";

            var emailClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, CapstoneProject)
            };

            await emailClient.SendMailAsync(new MailMessage(from: senderEmail, to: recieverEmail, subject, messageBody));
        }
    }
}

