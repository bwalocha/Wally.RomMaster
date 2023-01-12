﻿using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using Wally.RomMaster.FileService.Application.Users.Commands;
using Wally.RomMaster.FileService.Contracts.Requests.Users;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Filters;

namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Extensions;

public static class WebApiExtensions
{
	public static IServiceCollection AddWebApi(this IServiceCollection services)
	{
		services.AddControllers(settings => { settings.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
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

		services.AddValidatorsFromAssemblyContaining<UpdateUserRequestValidator>();
		services.AddValidatorsFromAssemblyContaining<UpdateUserCommandValidator>();
		services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);
		services.AddFluentValidationClientsideAdapters(); // TODO: consider config => config.ClientValidatorFactories

		return services;
	}

	public static IApplicationBuilder UseWebApi(this IApplicationBuilder app)
	{
		app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

		return app;
	}
}