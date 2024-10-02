using Wally.RomMaster.WolneLekturyService.Application;
using Wally.RomMaster.WolneLekturyService.Application.Contracts;
using Wally.RomMaster.WolneLekturyService.Application.MapperProfiles;
using Wally.RomMaster.WolneLekturyService.Application.Messages;
using Wally.RomMaster.WolneLekturyService.Domain;
using Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.WolneLekturyService.Infrastructure.Messaging;
using Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence;
using Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence.MySql;
using Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence.PostgreSQL;
using Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence.SqlServer;
using Wally.RomMaster.WolneLekturyService.Infrastructure.PipelineBehaviours;
using Wally.RomMaster.WolneLekturyService.Tests.ConventionTests.Helpers;
using Wally.RomMaster.WolneLekturyService.WebApi;

namespace Wally.RomMaster.WolneLekturyService.Tests.ConventionTests;

public static class Configuration
{
	public const string Namespace = "Wally.RomMaster.WolneLekturyService";

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
