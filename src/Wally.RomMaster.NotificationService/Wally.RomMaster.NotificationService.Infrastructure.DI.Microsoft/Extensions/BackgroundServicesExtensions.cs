using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wally.RomMaster.NotificationService.Infrastructure.BackgroundServices;
using Wally.RomMaster.NotificationService.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.NotificationService.Infrastructure.DI.Microsoft.Extensions;

public static class BackgroundServicesExtensions
{
	public static IServiceCollection AddBackgroundServices(this IServiceCollection services, AppSettings settings)
	{
		services.Scan(
			a => a.FromAssemblyOf<IInfrastructureBackgroundServicesAssemblyMarker>()
				.AddClasses(c => c.AssignableTo(typeof(IHostedService)))
				.As<IHostedService>()
				.WithSingletonLifetime());

		return services;
	}
}
