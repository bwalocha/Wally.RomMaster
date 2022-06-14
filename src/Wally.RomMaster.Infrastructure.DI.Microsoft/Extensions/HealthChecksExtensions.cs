using System.Threading;
using System.Threading.Tasks;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Wally.RomMaster.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft.Extensions;

public static class HealthChecksExtensions
{
	public static IServiceCollection AddHealthChecks(this IServiceCollection services, AppSettings settings)
	{
		var healthChecksBuilder = services.AddHealthChecks()
			.AddSqlServer(
				settings.ConnectionStrings.Database,
				name: "DB",
				failureStatus: HealthStatus.Degraded,
				tags: new[] { "DB", "Database", "MSSQL", })
			.AddVersionHealthCheck();

		services.AddHealthChecksUI() // TODO: Consider only for ApiGateway
			.AddInMemoryStorage();

		return services;
	}

	public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
	{
		app.UseHealthChecks(
			"/healthChecks",
			new HealthCheckOptions
			{
				Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
			});

		app.UseHealthChecksUI(); // TODO: Consider only for ApiGateway

		return app;
	}

	private static IHealthChecksBuilder AddVersionHealthCheck(this IHealthChecksBuilder builder)
	{
		builder.AddCheck<VersionHealthCheck>("VER", tags: new[] { "VER", "Version", });

		return builder;
	}

	private class VersionHealthCheck : IHealthCheck
	{
		private readonly string? _version;

		public VersionHealthCheck()
		{
			_version = GetType()
				.Assembly.GetName()
				.Version?.ToString();
		}

		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
		{
			return Task.FromResult(HealthCheckResult.Healthy(_version));
		}
	}
}
