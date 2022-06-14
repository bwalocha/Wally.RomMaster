using System;
using System.Security.Cryptography;

using Force.Crc32;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.BackgroundServices;
using Wally.RomMaster.BackgroundServices.Services;
using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft.Extensions;

public static class BackgroundServicesExtensions
{
	public static IServiceCollection AddBackgroundServices(this IServiceCollection services, AppSettings settings)
	{
		services.AddSingleton<HashAlgorithm, Crc32Algorithm>();
		services.AddSingleton<IClockService>(new ClockService(() => DateTime.UtcNow));

		services.AddHostedService(
			a => new FileWatcherService(a.GetRequiredService<ILogger<FileWatcherService>>(), settings, a));
		services.AddHostedService(
			a => new FileScannerService(
				a.GetRequiredService<ILogger<FileScannerService>>(),
				settings,
				a.GetRequiredService<IClockService>(),
				a));

		return services;
	}
}
