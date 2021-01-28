using System;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wally.Database;
using Wally.RomMaster.BusinessLogic.Services;
using Wally.RomMaster.Database;
using Wally.RomMaster.DatFileParser;
using Wally.RomMaster.Domain.Interfaces;
using Wally.RomMaster.Domain.Models;

namespace Wally.RomMaster
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddOptions().Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

			var appConfig = Configuration.GetSection(nameof(AppSettings));
			var appSettings = appConfig.Get<AppSettings>();

			services.AddRazorPages();
			services.AddServerSideBlazor();

			services
				.AddDbContext<DatabaseContext>(
					options =>
					{
						options
							.UseSqlite(appConfig.GetConnectionString("sqlite"))
							.EnableSensitiveDataLogging(appSettings.EnableSensitiveDataLogging);
					},
					ServiceLifetime.Singleton)
				.AddAutoMapper(GetType())
				.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>()
				.AddSingleton<Parser>()
				.AddSingleton<FileWatcherService>()
				.AddSingleton<DatFileService>()
				.AddSingleton<RomFileService>()
				.AddSingleton<ToSortFileService>()
				.AddSingleton<FixService>()
				.AddSingleton<HashAlgorithm, Force.Crc32.Crc32Algorithm>()
				.AddSingleton<IDebuggerService, DebuggerService>()
				.AddSingleton<IHostedService, ClientService>()
				.Replace(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(TimedLogger<>)));
		}

		public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			if (app == null)
			{
				throw new ArgumentNullException(nameof(app));
			}

			if (loggerFactory == null)
			{
				throw new ArgumentNullException(nameof(loggerFactory));
			}

			loggerFactory.AddProvider(app.ApplicationServices.GetService<IDebuggerService>().LoggerProvider);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days.
				// You may want to change this for production scenarios,
				// see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
				app.UseHttpsRedirection();
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.UseEndpoints(
				endpoints =>
				{
					endpoints.MapBlazorHub();
					endpoints.MapFallbackToPage("/_Host");
				});
		}
	}
}
