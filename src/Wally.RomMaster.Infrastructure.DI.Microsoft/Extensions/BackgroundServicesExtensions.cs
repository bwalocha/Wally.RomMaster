using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Wally.RomMaster.BackgroundServices;
using Wally.RomMaster.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft.Extensions;

public static class BackgroundServicesExtensions
{
	public static IServiceCollection AddBackgroundServices(this IServiceCollection services, AppSettings settings)
	{
		services.AddHostedService(
			a => new FileWatcherService(a.GetRequiredService<ILogger<FileWatcherService>>(), settings, a));
		services.AddHostedService(
			a => new FileScannerService(a.GetRequiredService<ILogger<FileScannerService>>(), settings));

		return services;
	}
}
