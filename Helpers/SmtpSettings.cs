using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailServicePackage
{
    internal class SmtpSettings
    {
        public string From { get; set; } = string.Empty;
        public string AppPassword { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}