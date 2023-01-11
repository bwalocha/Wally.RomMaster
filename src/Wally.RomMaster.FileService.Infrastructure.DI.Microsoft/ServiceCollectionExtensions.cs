using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Extensions;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppSettings settings)
	{
		services.AddWebApi();
		services.AddCqrs();
		services.AddSwagger(Assembly.GetCallingAssembly());
		services.AddHealthChecks(settings);
		services.AddDbContext(settings);
		services.AddMapper();
		services.AddMessaging(settings);
		services.AddEventHub();

		// services.AddBackgroundServices(settings);

		return services;
	}
}
