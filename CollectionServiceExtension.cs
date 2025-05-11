using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailServicePackage
{
    public static class CollectionServiceExtension
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
            return services.AddScoped<IEmailService, EmailService>();
        }
    }
}