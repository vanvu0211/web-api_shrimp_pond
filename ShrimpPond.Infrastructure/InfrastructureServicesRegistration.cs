using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Infrastructure.Logging;


namespace EquipmentManagement.Infrastructure
{
	public static class InfrastructureServicesRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
			return services;
		}
	}
}