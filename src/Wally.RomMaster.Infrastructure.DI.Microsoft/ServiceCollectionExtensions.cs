using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Wally.RomMaster.DatFileParser;
using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Infrastructure.DI.Microsoft.Extensions;
using Wally.RomMaster.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppSettings settings)
	{
		services.AddApiCors(settings.Cors);
		services.AddCqrs();
		services.AddSwagger(Assembly.GetCallingAssembly());
		services.AddHealthChecks(settings);
		services.AddDbContext(settings);
		services.AddMapper();
		services.AddEventHub();
		services.AddBackgroundServices(settings);

		services.AddScoped<IDataFileParser, Parser>();

		return services;
	}
}
