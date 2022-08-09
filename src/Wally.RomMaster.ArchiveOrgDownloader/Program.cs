using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

using Wally.RomMaster.ArchiveOrgDownloader.Models;
using Wally.RomMaster.DatFileParser;
using Wally.RomMaster.DatFileParser.MapperProfiles;
using Wally.RomMaster.Domain.Abstractions;

namespace Wally.RomMaster.ArchiveOrgDownloader;

[ExcludeFromCodeCoverage]
public static class Program
{
	private const bool _reloadOnChange = false;

	private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json", false, _reloadOnChange)
		.AddJsonFile(
			$"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
			true,
			_reloadOnChange)
		.AddJsonFile("serilog.json", true, _reloadOnChange)
		.AddJsonFile(
			$"serilog.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
			true,
			_reloadOnChange)
		.AddEnvironmentVariables()
		.Build();

	public static async Task<int> Main(string[] args)
	{
		Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration)
			.CreateLogger();

		try
		{
			Log.Information("Starting host...");
			var host = CreateHostBuilder(args)
				.Build();

			using (var scope = host.Services.CreateScope())
			{
				var action = scope.ServiceProvider.GetRequiredService<Downloader>();
				await action.ExecuteAsync(CancellationToken.None);
			}

			await host.RunAsync();
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Host terminated unexpectedly.");

			return 1;
		}
		finally
		{
			Log.CloseAndFlush();
		}

		return 0;
	}

	private static IHostBuilder CreateHostBuilder(string[] args)
	{
		return Host.CreateDefaultBuilder(args)
				.UseSerilog()
				.UseDefaultServiceProvider(opt => { opt.ValidateScopes = true; })
				.ConfigureServices(ConfigureServices)

			// .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
			;
	}

	private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
	{
		var appSettings = new AppSettings();
		Configuration.Bind(appSettings);

		services.AddAutoMapper(typeof(DataFileProfile).Assembly);
		services.AddTransient<IDataFileParser, Parser>();
		services.AddTransient<Downloader>();
		services.AddTransient(
			_ =>
			{
				var handler = new HttpClientHandler { AllowAutoRedirect = true, };

				return new HttpClient(handler);
			});
		services.AddTransient(_ => appSettings);
	}
}
