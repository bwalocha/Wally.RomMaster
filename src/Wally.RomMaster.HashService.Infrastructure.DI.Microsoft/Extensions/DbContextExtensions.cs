using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

using Wally.RomMaster.HashService.Domain.Abstractions;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Providers;
using Wally.RomMaster.HashService.Infrastructure.Persistence;
using Wally.RomMaster.HashService.Infrastructure.Persistence.MySql;
using Wally.RomMaster.HashService.Infrastructure.Persistence.PostgreSQL;
using Wally.RomMaster.HashService.Infrastructure.Persistence.SqlServer;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Extensions;

public static class DbContextExtensions
{
	public static IServiceCollection AddDbContext(this IServiceCollection services, AppSettings settings)
	{
		Action<DbContextOptionsBuilder> dbContextOptions;
		dbContextOptions = options =>
		{
			switch (settings.Database.ProviderType)
			{
				case DatabaseProviderType.MySql:
					options.UseMySql(
						settings.ConnectionStrings.Database,
						MySqlServerVersion.LatestSupportedServerVersion,
						builder =>
						{
							builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
							builder.MigrationsAssembly(
								typeof(IInfrastructureMySqlAssemblyMarker).Assembly.GetName()
									.Name);
						});
					break;
				case DatabaseProviderType.PostgreSQL:
					options.UseNpgsql(
						settings.ConnectionStrings.Database,
						builder =>
						{
							builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
							builder.MigrationsAssembly(
								typeof(IInfrastructurePostgreSQLAssemblyMarker).Assembly.GetName()
									.Name);
						});
					break;
				case DatabaseProviderType.SqlServer:
					options.UseSqlServer(
						settings.ConnectionStrings.Database,
						builder =>
						{
							builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
							builder.MigrationsAssembly(
								typeof(IInfrastructureSqlServerAssemblyMarker).Assembly.GetName()
									.Name);
						});
					break;
				default:
					throw new ArgumentOutOfRangeException(
						nameof(settings.Database.ProviderType),
						"Unknown Database Provider Type");
			}

			options.ConfigureWarnings(
				builder =>
				{
					builder.Default(WarningBehavior.Throw);
					builder.Ignore(RelationalEventId.MultipleCollectionIncludeWarning);
					builder.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS);
					builder.Log(CoreEventId.SensitiveDataLoggingEnabledWarning);
				});

			options.EnableSensitiveDataLogging(); // TODO: get from configuration
		};
		services.AddDbContext<DbContext, ApplicationDbContext>(dbContextOptions);

		services.Scan(
			a => a.FromApplicationDependencies(b => b.FullName!.StartsWith("Wally.RomMaster.HashService."))
				.AddClasses(c => c.AssignableTo(typeof(IReadOnlyRepository<>)))
				.AsImplementedInterfaces()
				.WithScopedLifetime());

		services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
		services.AddScoped<IUserProvider, HttpUserProvider>();

		return services;
	}

	public static IApplicationBuilder UseDbContext(
		this IApplicationBuilder app,
		DbContext dbContext,
		DbContextSettings settings)
	{
		if (settings.IsMigrationEnabled)
		{
			dbContext.Database.Migrate();
		}

		return app;
	}
}
