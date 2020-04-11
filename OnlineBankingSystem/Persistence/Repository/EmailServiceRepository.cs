using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using OnlineBankingSystem.Core.Models;
using OnlineBankingSystem.Core.Repositories;
using System.Configuration;

namespace OnlineBankingSystem.Persistence.Repository
{
    public class EmailServiceRepository : IEmailService
    {
        public void SendEmail(string EmailTo, string user, string subject, string content)
        {
            var f = new EmailAddress()
            {
                Name = ConfigurationManager.AppSettings["CompanyName"],
                Address = ConfigurationManager.AppSettings["CompanyEmail"]
            };
            var T = new EmailAddress()
            {
                Name = user,
                Address = EmailTo
            };

            EmailMessage cv = new EmailMessage()
            {
                FromAddresses = f,
                ToAddresses = T,
                Content = content,
                Subject = subject
            };

            MimeMessage message = new MimeMessage();

            string a = cv.FromAddresses.Name.ToString();
            string b = cv.FromAddresses.Address.ToString();
            MailboxAddress c = new MailboxAddress(a, b);
            message.From.Add(c);
            message.To.Add(new MailboxAddress(cv.ToAddresses.Name, cv.ToAddresses.Address));


            message.Subject = subject;
            //  We will say we are sending HTML.But there are options for plaintext etc. 

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = content
            };

            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
            using (var emailClient = new SmtpClient())
            {
                // The last parameter here is to use SSL(Which you should!)

                emailClient.Connect(ConfigurationManager.AppSettings["SmtpServer"], int.Parse(ConfigurationManager.AppSettings["SmtpPort"]), false);

                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(ConfigurationManager.AppSettings["SmtpUsername"], ConfigurationManager.AppSettings["SmtpPassword"]);

                emailClient.Send(message);

                emailClient.Disconnect(true);
            }
        }
    }
}