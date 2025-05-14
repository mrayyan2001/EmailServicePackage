using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EmailServicePackage.Helpers;
using EmailServicePackage.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailServicePackage.Services
{
    internal class SendGridService : ISendGridService
    {
        private readonly SendGridSettings _sendGridSettings;

        public SendGridService(IOptions<SendGridSettings> sendGridSettings)
        {
            _sendGridSettings = sendGridSettings.Value;
        }

        public async Task SendEmailAsync(SendEmailDto dto)
        {
            var client = new SendGridClient(_sendGridSettings.ApiKey);

            var msg = MailHelper.CreateSingleEmail(
                from: new EmailAddress(_sendGridSettings.From, _sendGridSettings.FromName),
                to: new EmailAddress(dto.To, dto.ToName),
                subject: dto.Subject,
                plainTextContent: dto.Body,
                htmlContent: $"<p>{dto.Body}</p>"
                );

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                var errorBody = await response.Body.ReadAsStringAsync();
                throw new Exception($"Failed to send email. Status: {response.StatusCode}, Response: {errorBody}");
            }
        }
    }
}