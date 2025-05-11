using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailServicePackage
{
    public interface IEmailService
    {
        public Task SendEmailAsync(SendEmailDto dto);
    }
}