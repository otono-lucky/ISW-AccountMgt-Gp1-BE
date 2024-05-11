using AccountMgt.Model.DTO;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AccountMgt.Utility.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailRegistration(EmailDto request)
        {
            string body = PopulateRegisterEmail(request.UserName, request.Otp);
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:SmtpUsername"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            /*email.Body = new TextPart(TextFormat.Html) { Text = request.Body };*/
            var builder = new BodyBuilder();
            builder.HtmlBody = body;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_config["EmailSettings:SmtpHost"], int.Parse(_config["EmailSettings:SmtpPort"]), SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_config["EmailSettings:SmtpUsername"], _config["EmailSettings:SmtpPassword"]);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        private string PopulateRegisterEmail(string UserName, string Otp)
        {
            string body = string.Empty;
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(rootPath, "..", "..", "Templates", "RegistrationTemplate.html");
            using (StreamReader reader = new StreamReader(filePath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{otp}", Otp);
            return body;
        }

        public async Task SendLockoutNotificationAsync(EmailDto request)
        {
            var body = PopulateLockedOutNotification();
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:SmtpUsername"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            /*email.Body = new TextPart(TextFormat.Html) { Text = request.Body };*/
            var builder = new BodyBuilder();
            builder.HtmlBody = body;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_config["EmailSettings:SmtpHost"], int.Parse(_config["EmailSettings:SmtpPort"]), SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_config["EmailSettings:SmtpUsername"], _config["EmailSettings:SmtpPassword"]);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        private string PopulateLockedOutNotification()
        {
            string body = string.Empty;
            string filePath = Directory.GetCurrentDirectory() + @"\Templates\LockedOutNotification.html";

            using (StreamReader reader = new StreamReader(filePath))
            {
                body = reader.ReadToEnd();
            }
            return body;
        }

        public async Task SendForgotPasswordEmailAsync(EmailDto request)
        {
            string body = PopulateRegisterEmail(request.UserName, request.Otp);
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:SmtpUsername"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };
            /*var builder = new BodyBuilder();
            builder.HtmlBody = body;

            email.Body = builder.ToMessageBody();*/

            using var smtp = new SmtpClient();

            smtp.Connect(_config["EmailSettings:SmtpHost"], int.Parse(_config["EmailSettings:SmtpPort"]), SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_config["EmailSettings:SmtpUsername"], _config["EmailSettings:SmtpPassword"]);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        
    }
}
