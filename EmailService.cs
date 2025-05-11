using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EmailServicePackage
{
    internal class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(SendEmailDto dto)
        {
            string from = _emailSettings.From;
            string appPassword = _emailSettings.AppPassword;
            string host = _emailSettings.Host;
            int port = _emailSettings.Port;

            var client = new SmtpClient(host, port)
            {
                EnableSsl = true, // connection encrypted
                Credentials = new NetworkCredential(from, appPassword) // email, password
            };

            var mail = new MailMessage(from, dto.to, dto.subject, dto.body);
            await client.SendMailAsync(mail);
        }
    }
}