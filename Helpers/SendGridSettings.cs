using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailServicePackage.Helpers
{
    internal class SendGridSettings
    {
        public string From { get; set; } = string.Empty;
        public string? FromName { get; set; }
        public string ApiKey { get; set; } = string.Empty;
    }
}