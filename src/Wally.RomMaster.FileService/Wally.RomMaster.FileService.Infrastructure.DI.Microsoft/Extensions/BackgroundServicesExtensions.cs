using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Abstractions;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Services;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Extensions;

public static class BackgroundServicesExtensions
{
	public static IServiceCollection AddBackgroundServices(this IServiceCollection services, AppSettings settings)
	{
		services.AddSingleton<IUserProvider, ServiceUserProvider>();
		services.AddSingleton<ISettings>(settings);
		services.AddSingleton<IClockService>(new ClockService(() => DateTime.UtcNow));

		services.Scan(
			a => a.FromAssemblyOf<IInfrastructureBackgroundServicesAssemblyMarker>()
				.AddClasses(c => c.AssignableTo(typeof(BackgroundService)))
				.As<IHostedService>()
				.WithSingletonLifetime());

		return services;
	}
}
