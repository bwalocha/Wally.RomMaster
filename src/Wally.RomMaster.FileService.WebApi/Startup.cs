using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Extensions;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.FileService.WebApi.Hubs;

namespace Wally.RomMaster.FileService.WebApi;

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
	///     Gets Application Settings data
	/// </summary>
	public AppSettings AppSettings { get; } = new();

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		Configuration.Bind(AppSettings);

		services.AddInfrastructure(AppSettings);
	}

	public void Configure(
		IApplicationBuilder app,
		IWebHostEnvironment env,
		IHostApplicationLifetime appLifetime,
		ILogger<Startup> logger,
		DbContext dbContext)
	{
		appLifetime.ApplicationStarted.Register(
			() => logger.LogInformation("The 'Wally.RomMaster.FileService' is started"));
		appLifetime.ApplicationStopping.Register(
			() => logger.LogInformation("The 'Wally.RomMaster.FileService' is stopping"));
		appLifetime.ApplicationStopped.Register(
			() => logger.LogInformation("The 'Wally.RomMaster.FileService' is stopped"));

		// Configure the HTTP request pipeline.
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger(AppSettings.SwaggerAuthentication);
		}

		// If the App is hosted by Docker, HTTPS is not required inside container
		// app.UseHttpsRedirection();

		app.UseRouting();

		// app.UseAuthentication(); // TODO: Consider only for ApiGateway
		app.UseAuthorization();
		app.UseHealthChecks();

		app.UseWebApi();

		app.UseDbContext(dbContext, AppSettings.Database);
		app.UseMessaging();
		app.UseEventHub<EventHub>();
	}
}