using Microsoft.Extensions.DependencyInjection;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Extensions;

public static class BackgroundServicesExtensions
{
	public static IServiceCollection AddBackgroundServices(this IServiceCollection services, AppSettings settings)
	{
		/*
		services.AddHostedService(?);
		services.AddHostedService(?);
		*/

		/*
		services.Scan(
			a => a.FromAssemblyOf<?>()
				.AddClasses(c => c.AssignableTo(typeof(BackgroundService)))
				.AsImplementedInterfaces()
				.WithScopedLifetime());
		*/

		return services;
	}
}
