using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Wally.RomMaster.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft.Extensions;

public static class ApiCorsExtensions
{
	private const string CorsPolicy = nameof(CorsPolicy);

	public static IServiceCollection AddApiCors(this IServiceCollection services, CorsSettings settings)
	{
		services.AddCors(
			options => options.AddPolicy(
				CorsPolicy,
				builder => builder.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
					.AllowAnyHeader()
					.AllowCredentials()
					.WithOrigins(settings.Origins.ToArray())));

		return services;
	}

	public static IApplicationBuilder UseApiCors(this IApplicationBuilder app)
	{
		app.UseCors(CorsPolicy);

		return app;
	}
}
