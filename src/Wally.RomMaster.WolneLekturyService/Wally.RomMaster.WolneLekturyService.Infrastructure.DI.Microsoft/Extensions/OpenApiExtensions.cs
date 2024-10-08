﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Swagger;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Extensions;

public static class OpenApiExtensions
{
	public static IServiceCollection AddOpenApi(this IServiceCollection services, Assembly assembly)
	{
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
		// services.AddODataApiExplorer();
		services.AddSwaggerGen(
			options =>
			{
				options.SwaggerDoc(
					"v1",
					new OpenApiInfo
					{
						Version = "v1",
						Title = "Wally.RomMaster API",
						Description = "An ASP.NET Core Web API for managing 'Wally.RomMaster' items",

						// TermsOfService = new Uri("https://example.com/terms"),
						Contact = new OpenApiContact
						{
							Name = "Wally",
							Email = "b.walocha@gmail.com",
							Url = new Uri("https://wally.best"),
						},
						License = new OpenApiLicense
						{
							Name = "MIT",
							Url = new Uri("https://opensource.org/licenses/MIT"),
						},
					});

				var xmlFilename = $"{assembly.GetName().Name}.xml";
				options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

				options.OperationFilter<ResponseTypesOperationFilter>();
				options.OperationFilter<ODataQueryOptionsOperationFilter>();

				options.DocumentFilter<SchemasFilter>();
			});

		return services;
	}

	public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
	{
		app.UseSwagger(setupAction: null)
			.UseSwaggerUI(
				options =>
				{
					var settings = app.ApplicationServices.GetRequiredService<IOptions<AppSettings>>();

					// options.SwaggerEndpoint("v1/swagger.json", "Wally.RomMaster WebApi v1");
					options.OAuthClientId(settings.Value.SwaggerAuthentication.ClientId);
					options.OAuthClientSecret(settings.Value.SwaggerAuthentication.ClientSecret);

					// TODO:
					// options.OAuthScopes(string.Join(", ", settings.Value.SwaggerAuthentication.Scopes.Values));
					options.OAuthUsePkce();

					options.OAuthAppName("Wally.RomMaster");
					options.EnablePersistAuthorization();
					options.DefaultModelsExpandDepth(0);
				});

		return app;
	}
}
