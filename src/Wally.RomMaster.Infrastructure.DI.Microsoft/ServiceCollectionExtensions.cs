using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Wally.RomMaster.Infrastructure.DI.Microsoft.Extensions;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext(configuration);

		return services;
	}
}
