﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wally.RomMaster.HashService.Domain.Abstractions;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Providers;
using Wally.RomMaster.HashService.Infrastructure.Persistence;
using Wally.RomMaster.HashService.Infrastructure.Persistence.MySql;
using Wally.RomMaster.HashService.Infrastructure.Persistence.PostgreSQL;
using Wally.RomMaster.HashService.Infrastructure.Persistence.SQLite;
using Wally.RomMaster.HashService.Infrastructure.Persistence.SqlServer;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Extensions;

public static class PersistenceExtensions
{
	public static IServiceCollection AddAddPersistence(this IServiceCollection services, AppSettings settings)
	{
		Action<DbContextOptionsBuilder> dbContextOptions = options =>
		{
			switch (settings.Database.ProviderType)
			{
				case DatabaseProviderType.None:
					break;
				case DatabaseProviderType.InMemory:
					WithInMemory(options);
					break;
				case DatabaseProviderType.MySql:
					WithMySql(options, settings);
					break;
				case DatabaseProviderType.PostgreSQL:
					WithPostgreSQL(options, settings);
					break;
				case DatabaseProviderType.SQLite:
					WithSQLite(options, settings);
					break;
				case DatabaseProviderType.SqlServer:
					WithSqlServer(options, settings);
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
			a => a.FromAssemblyOf<IInfrastructurePersistenceAssemblyMarker>()
				.AddClasses(c => c.AssignableTo(typeof(IReadOnlyRepository<,>)))
				.AsImplementedInterfaces()
				.WithScopedLifetime());

		services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
		services.AddScoped<IUserProvider, HttpUserProvider>();

		return services;
	}

	private static void WithInMemory(DbContextOptionsBuilder options)
	{
		options.UseInMemoryDatabase(nameof(DatabaseProviderType.InMemory), builder => builder.EnableNullChecks());
	}

	private static void WithMySql(DbContextOptionsBuilder options, AppSettings settings)
	{
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
		EntityFramework.Exceptions.MySQL.Pomelo.ExceptionProcessorExtensions.UseExceptionProcessor(options);
	}

	private static void WithPostgreSQL(DbContextOptionsBuilder options, AppSettings settings)
	{
		options.UseNpgsql(
			settings.ConnectionStrings.Database,
			builder =>
			{
				builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
				builder.MigrationsAssembly(
					typeof(IInfrastructurePostgreSqlAssemblyMarker).Assembly.GetName()
						.Name);
			});
		EntityFramework.Exceptions.PostgreSQL.ExceptionProcessorExtensions.UseExceptionProcessor(options);
	}

	private static void WithSQLite(DbContextOptionsBuilder options, AppSettings settings)
	{
		options.UseSqlite(
			settings.ConnectionStrings.Database,
			builder =>
			{
				builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
				builder.MigrationsAssembly(
					typeof(IInfrastructureSQLiteAssemblyMarker).Assembly.GetName()
						.Name);
			});
		EntityFramework.Exceptions.Sqlite.ExceptionProcessorExtensions.UseExceptionProcessor(options);
	}

	private static void WithSqlServer(DbContextOptionsBuilder options, AppSettings settings)
	{
		options.UseSqlServer(
			settings.ConnectionStrings.Database,
			builder =>
			{
				builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
				builder.MigrationsAssembly(
					typeof(IInfrastructureSqlServerAssemblyMarker).Assembly.GetName()
						.Name);
			});
		EntityFramework.Exceptions.SqlServer.ExceptionProcessorExtensions.UseExceptionProcessor(options);
	}

	public static IApplicationBuilder UsePersistence(this IApplicationBuilder app)
	{
		var settings = app.ApplicationServices.GetRequiredService<IOptions<AppSettings>>();

		if (!settings.Value.Database.IsMigrationEnabled ||
			settings.Value.Database.ProviderType == DatabaseProviderType.None ||
			settings.Value.Database.ProviderType == DatabaseProviderType.InMemory)
		{
			return app;
		}

		using var scope = app.ApplicationServices.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
		dbContext.Database.Migrate();

		return app;
	}
}
