using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailServicePackage.Helpers;
using EmailServicePackage.Interfaces;
using EmailServicePackage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailServicePackage
{
    public static class CollectionServiceExtension
    {
        public static IServiceCollection AddSmtpService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SmtpSettings>(config.GetSection("SmtpSettings"));
            return services.AddScoped<ISmtpService, SmtpService>();
        }

        public static IServiceCollection AddSendGridService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SendGridSettings>(config.GetSection("SendGridSettings"));
            return services.AddScoped<ISendGridService, SendGridService>();
        }
    }
}