using AutoMapper.Extensions.ExpressionMapping;

using Microsoft.Extensions.DependencyInjection;

using Wally.RomMaster.MapperProfiles;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft.Extensions;

public static class AutoMapperExtensions
{
	public static IServiceCollection AddMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); }, typeof(UserProfile).Assembly);

		return services;
	}
}
