using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Wally.RomMaster.Application.Users.Commands;
using Wally.RomMaster.Contracts.Requests.Users;
using Wally.RomMaster.Infrastructure.DI.Microsoft;
using Wally.RomMaster.Infrastructure.DI.Microsoft.Extensions;
using Wally.RomMaster.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.WebApi.Filters;
using Wally.RomMaster.WebApi.Hubs;

namespace Wally.RomMaster.WebApi;

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

		services.AddControllers(settings => { settings.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
			.AddFluentValidation(
				config =>
				{
					config.ImplicitlyValidateChildProperties = true;
					config.RegisterValidatorsFromAssemblyContaining<UpdateUserRequestValidator>(
						lifetime: ServiceLifetime.Singleton);
					config.RegisterValidatorsFromAssemblyContaining<UpdateUserCommandValidator>(
						lifetime: ServiceLifetime.Singleton);
				})
			.AddOData(
				options =>
				{
					options.Filter()
						.OrderBy()
						.Count()
						.SetMaxTop(1000);
				})
			.AddNewtonsoftJson(
				options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

		services.AddInfrastructure(AppSettings);
	}

	public void Configure(
		IApplicationBuilder app,
		IWebHostEnvironment env,
		IHostApplicationLifetime appLifetime,
		ILogger<Startup> logger,
		DbContext dbContext)
	{
		appLifetime.ApplicationStarted.Register(() => logger.LogInformation("The 'Wally.RomMaster' is started"));
		appLifetime.ApplicationStopping.Register(() => logger.LogInformation("The 'Wally.RomMaster' is stopping"));
		appLifetime.ApplicationStopped.Register(() => logger.LogInformation("The 'Wally.RomMaster' is stopped"));

		// Configure the HTTP request pipeline.
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger(AppSettings.SwaggerAuthentication);
		}

		// If the App is hosted by Docker, HTTPS is not required inside container
		// app.UseHttpsRedirection();

		app.UseApiCors();
		app.UseRouting();
		app.UseAuthorization();
		app.UseHealthChecks();
		app.UseEndpoints(
			endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapGet(
					"/",
					async context =>
					{
						await context.Response.WriteAsync(
							$"v{GetType().Assembly.GetName().Version}",
							context.RequestAborted);
					});
			});

		app.UseDbContext(dbContext, AppSettings.Database);
		app.UseEventHub<EventHub>();
	}
}
