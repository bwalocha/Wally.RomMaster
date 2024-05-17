using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wally.RomMaster.HashService.Infrastructure.BackgroundServices;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Extensions;

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
