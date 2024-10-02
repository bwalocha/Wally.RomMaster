using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.DependencyInjection;
using Wally.RomMaster.WolneLekturyService.Application.MapperProfiles;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Extensions;

public static class MapperExtensions
{
	public static IServiceCollection AddMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); },
			typeof(IApplicationMapperProfilesAssemblyMarker).Assembly);

		return services;
	}
}
