using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailServicePackage.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EmailServicePackage
{
    internal class SmtpService : ISmtpService
    {
        private readonly SmtpSettings _smtpSettings;

        public SmtpService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(SendEmailDto dto)
        {
            string from = _smtpSettings.From;
            string appPassword = _smtpSettings.AppPassword;
            string host = _smtpSettings.Host;
            int port = _smtpSettings.Port;

            var client = new SmtpClient(host, port)
            {
                EnableSsl = true, // connection encrypted
                Credentials = new NetworkCredential(from, appPassword) // email, password
            };

            var mail = new MailMessage(from, dto.To, dto.Subject, dto.Body);
            await client.SendMailAsync(mail);
        }
    }
}