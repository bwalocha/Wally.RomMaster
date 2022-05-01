using System;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Wally.RomMaster.Infrastructure.DI.Microsoft.Models;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft.Extensions;

public static class SwaggerExtensions
{
	public static IServiceCollection AddSwagger(this IServiceCollection services, Assembly assembly)
	{
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
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
							Name = "Wally", Email = "b.walocha@gmail.com", Url = new Uri("https://wally.best"),
						},
						License = new OpenApiLicense
						{
							Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT"),
						},
					});

				var xmlFilename = $"{assembly.GetName().Name}.xml";
				options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

				/*options.DocumentFilter<ODataCommonDocumentFilter>();*/
			});

		return services;
	}

	public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, AuthenticationSettings settings)
	{
		app.UseSwagger();
		app.UseSwaggerUI(
			opt =>
			{
				// opt.SwaggerEndpoint("v1/swagger.json", "Wally.RomMaster WebApi v1");
				opt.OAuthClientId(settings.ClientId);
				opt.OAuthClientSecret(settings.ClientSecret);
				opt.OAuthUsePkce();
			});

		return app;
	}

	/*private class ODataCommonDocumentFilter : IDocumentFilter
	{
		public static readonly string[] FilteredOutSchemaTypes =
		{
			nameof(IEdmType),
			nameof(IEdmTypeReference),
			nameof(IEdmTerm),
			nameof(IEdmEntityContainer),
			nameof(IEdmModel),
			nameof(IEdmSchemaElement),
			nameof(IEdmDirectValueAnnotationsManager),
			nameof(IEdmVocabularyAnnotatable),
			nameof(IEdmVocabularyAnnotation),
			nameof(IEdmExpression),
			nameof(IEdmEntityContainerElement),

			nameof(ODataEntitySetInfo),
			nameof(ODataFunctionImportInfo),
			nameof(ODataServiceDocument),
			nameof(ODataSingletonInfo),
			nameof(ODataTypeAnnotation),
		};

		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			foreach (var description in context.ApiDescriptions)
			{
				if (description.RelativePath.EndsWith("$count", StringComparison.CurrentCultureIgnoreCase) ||
					"Metadata".Equals((description.ActionDescriptor as ControllerActionDescriptor)?.ControllerName, StringComparison.CurrentCultureIgnoreCase))
				{
					var route = "/" + description.RelativePath;
					_ = swaggerDoc.Paths.Remove(route);
				}
			}

			foreach (var schema in FilteredOutSchemaTypes)
			{
				_ = swaggerDoc.Components.Schemas.Remove(schema);
			}
		}
	}*/
}
