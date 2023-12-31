﻿using CapstoneProject.Application.Services.Abstractions;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace CapstoneProject.Application.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string toAddress, string subject, string body)
        {
            string smtpHost = _configuration["SmtpSettings:Host"];
            int smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
            string smtpUsername = _configuration["SmtpSettings:Username"];
            string smtpPassword = _configuration["SmtpSettings:Password"];
            bool enableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = enableSsl;

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(smtpUsername);
                mailMessage.To.Add(toAddress);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                smtpClient.Send(mailMessage);
            }
        }

        /*public async Task CreateEmail(string recieverEmail, string subject, string messageBody)
        {
            var senderEmail = "owora.chukwuemeka@gmail.com";
            var CapstoneProject = "pgbnqnaadkynbajc";

            var emailClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, CapstoneProject)
            };

            await emailClient.SendMailAsync(new MailMessage(from: senderEmail, to: recieverEmail, subject, messageBody));
        }*/
    }
}

