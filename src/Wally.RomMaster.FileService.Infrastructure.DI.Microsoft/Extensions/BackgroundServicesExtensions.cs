using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Wally.RomMaster.FileService.BackgroundServices;
using Wally.RomMaster.FileService.BackgroundServices.Abstractions;
using Wally.RomMaster.FileService.BackgroundServices.Services;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Extensions;

public static class BackgroundServicesExtensions
{
	public static IServiceCollection AddBackgroundServices(this IServiceCollection services, AppSettings settings)
	{
		services.AddSingleton<IUserProvider, ServiceUserProvider>();
		services.AddSingleton<ISettings>(settings);
		services.AddSingleton<IClockService>(new ClockService(() => DateTime.UtcNow));

		// services.AddHostedService<FileScannerService>();
		// services.AddHostedService<FileWatcherService>();

		services.Scan(
			a => a.FromAssemblyOf<FileScannerService>()
				.AddClasses(c => c.AssignableTo(typeof(BackgroundService)))
				.As<IHostedService>()
				.WithSingletonLifetime());

		return services;
	}
}
