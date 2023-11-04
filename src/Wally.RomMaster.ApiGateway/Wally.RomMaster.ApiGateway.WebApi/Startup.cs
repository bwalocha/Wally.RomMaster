using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft;
using Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.ApiGateway.WebApi;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddInfrastructure(Configuration);
	}

	public void Configure(
		IApplicationBuilder app,
		IWebHostEnvironment env,
		IHostApplicationLifetime appLifetime,
		ILogger<Startup> logger,
		IOptions<AppSettings> options)
	{
		appLifetime.ApplicationStarted.Register(
			() => logger.LogInformation("The 'Wally.RomMaster.ApiGateway.WebApi' is started"));
		appLifetime.ApplicationStopping.Register(
			() => logger.LogInformation("The 'Wally.RomMaster.ApiGateway.WebApi' is stopping"));
		appLifetime.ApplicationStopped.Register(
			() => logger.LogInformation("The 'Wally.RomMaster.ApiGateway.WebApi' is stopped"));

		app.UseInfrastructure(env, options);
	}
}