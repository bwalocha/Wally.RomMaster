using System.Security.Cryptography;

using Force.Crc32;

using Microsoft.Extensions.DependencyInjection;

using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Extensions;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppSettings settings)
	{
		// services.AddWebApi();
		services.AddCqrs();

		// services.AddSwagger(Assembly.GetCallingAssembly());
		services.AddHealthChecks(settings);

		// services.AddDbContext(settings);
		// services.AddMapper();
		services.AddMessaging(settings);
		services.AddEventHub();

		services.AddSingleton<HashAlgorithm, Crc32Algorithm>();

		return services;
	}
}
