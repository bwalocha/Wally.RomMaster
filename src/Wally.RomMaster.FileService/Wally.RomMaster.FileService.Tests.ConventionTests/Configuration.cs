using Wally.RomMaster.FileService.Application;
using Wally.RomMaster.FileService.Application.Contracts;
using Wally.RomMaster.FileService.Application.MapperProfiles;
using Wally.RomMaster.FileService.Application.Messages;
using Wally.RomMaster.FileService.Domain;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.FileService.Infrastructure.Messaging;
using Wally.RomMaster.FileService.Infrastructure.Persistence;
using Wally.RomMaster.FileService.Infrastructure.Persistence.MySql;
using Wally.RomMaster.FileService.Infrastructure.Persistence.PostgreSQL;
using Wally.RomMaster.FileService.Infrastructure.Persistence.SqlServer;
using Wally.RomMaster.FileService.Infrastructure.PipelineBehaviours;
using Wally.RomMaster.FileService.Tests.ConventionTests.Helpers;
using Wally.RomMaster.FileService.WebApi;

namespace Wally.RomMaster.FileService.Tests.ConventionTests;

public static class Configuration
{
	public static Types Types
		=> new()
		{
			AppSettings = new[]
			{
				typeof(AppSettings),
			},
		};

	public static Assemblies Assemblies
		=> new()
		{
			Application = new[]
			{
				typeof(IApplicationAssemblyMarker).Assembly,
				typeof(IApplicationContractsAssemblyMarker).Assembly,
				typeof(IApplicationMapperProfilesAssemblyMarker).Assembly,
				typeof(IApplicationMessagesAssemblyMarker).Assembly,
			},
			Domain = new[]
			{
				typeof(IDomainAssemblyMarker).Assembly,
			},
			Infrastructure = new[]
			{
				typeof(IInfrastructureDIMicrosoftAssemblyMarker).Assembly,
				typeof(IInfrastructureMessagingAssemblyMarker).Assembly,
				typeof(IInfrastructurePersistenceAssemblyMarker).Assembly,
				typeof(IInfrastructureSqlServerAssemblyMarker).Assembly,
				typeof(IInfrastructurePostgreSqlAssemblyMarker).Assembly,
				typeof(IInfrastructureMySqlAssemblyMarker).Assembly,
				typeof(IInfrastructurePipelineBehavioursAssemblyMarker).Assembly,
			},
			Presentation = new[]
			{
				typeof(IPresentationAssemblyMarker).Assembly,
			},
		};
}
