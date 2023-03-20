using System;

using Microsoft.Extensions.DependencyInjection;

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

		return services;
	}
}
