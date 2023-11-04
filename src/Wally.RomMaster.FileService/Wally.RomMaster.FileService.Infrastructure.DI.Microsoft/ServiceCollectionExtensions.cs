﻿using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Extensions;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Hubs;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var settings = new AppSettings();
		configuration.Bind(settings);

		services.AddOptions(settings);
		services.AddWebApi();
		services.AddCqrs();
		services.AddSwagger(Assembly.GetCallingAssembly());
		services.AddHealthChecks(settings);
		services.AddDbContext(settings);
		services.AddMapper();
		services.AddMessaging(settings);
		services.AddEventHub();
		// services.AddBackgroundServices(settings); // This applying is postponed in Program.cs 

		return services;
	}

	public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env)
	{
		// Configure the HTTP request pipeline.
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
		}

		// If the App is hosted by Docker, HTTPS is not required inside container
		// app.UseHttpsRedirection();

		app.UseRouting();

		// app.UseAuthentication(); // TODO: Consider only for ApiGateway
		app.UseAuthorization();
		app.UseHealthChecks();

		app.UseWebApi();

		app.UseDbContext();
		app.UseEventHub<EventHub>();

		return app;
	}
}