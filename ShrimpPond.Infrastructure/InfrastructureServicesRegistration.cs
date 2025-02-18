using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShrimpPond.Application.Contract.EmailService;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Models.Email;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Infrastructure.EmailService;
using ShrimpPond.Infrastructure.GmailService;
using ShrimpPond.Infrastructure.Logging;


namespace ShrimpPond.Infrastructure
{
	public static class InfrastructureServicesRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<GmailSettings>(configuration.GetSection("GmailSettings"));
            services.AddTransient<IGmailSender, GmailSender>();
            return services;
		}
	}
}