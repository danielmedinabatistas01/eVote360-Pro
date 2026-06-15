using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Settings;
using eVote360Pro.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentApp.Infrastructure.Shared
{
    public static class ServicesRegistration
    {
        //Extension method - Decorator pattern
        public static void AddSharedLayerIoc(this IServiceCollection services, IConfiguration config)
        {
            #region Configurations
            services.Configure<MailSettings>(config.GetSection("MailSettings"));
            #endregion

            #region Services IOC
            services.AddScoped<IEmailService, EmailService>();
            #endregion
        }
    }
}
