using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailServicePackage
{
    public class SendEmailDto
    {
        public string To { get; set; } = string.Empty;
        public string? ToName { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}