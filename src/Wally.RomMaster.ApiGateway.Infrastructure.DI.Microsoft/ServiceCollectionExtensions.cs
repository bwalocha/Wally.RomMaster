using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft.Extensions;
using Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration,
		AppSettings settings)
	{
		services.AddReverseProxy(configuration);
		services.AddHealthChecks(settings);
		services.AddApiCors(settings.Cors);

		return services;
	}
}
