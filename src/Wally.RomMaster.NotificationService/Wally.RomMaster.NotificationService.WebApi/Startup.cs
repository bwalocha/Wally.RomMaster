using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.NotificationService.Infrastructure.DI.Microsoft;

namespace Wally.RomMaster.NotificationService.WebApi;

/// <summary>
/// The Startup class.
/// </summary>
public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	/// <summary>
	///     Gets Configuration data
	/// </summary>
	public IConfiguration Configuration { get; }

	/// <summary>
	/// This method gets called by the runtime. Use this method to add services to the container. 
	/// </summary>
	/// <param name="services">The Service Collection.</param>
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddInfrastructure(Configuration);
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime,
		ILogger<Startup> logger)
	{
		appLifetime.ApplicationStarted.Register(() =>
			logger.LogInformation("The 'Wally.RomMaster.NotificationService' is started"));
		appLifetime.ApplicationStopping.Register(() =>
			logger.LogInformation("The 'Wally.RomMaster.NotificationService' is stopping"));
		appLifetime.ApplicationStopped.Register(() =>
			logger.LogInformation("The 'Wally.RomMaster.NotificationService' is stopped"));

		app.UseInfrastructure(env);
	}
}