using Wally.RomMaster.HashService.Application;
using Wally.RomMaster.HashService.Application.Contracts;
using Wally.RomMaster.HashService.Application.MapperProfiles;
using Wally.RomMaster.HashService.Application.Messages;
using Wally.RomMaster.HashService.Domain;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.HashService.Infrastructure.Messaging;
using Wally.RomMaster.HashService.Infrastructure.Persistence;
using Wally.RomMaster.HashService.Infrastructure.Persistence.MySql;
using Wally.RomMaster.HashService.Infrastructure.Persistence.PostgreSQL;
using Wally.RomMaster.HashService.Infrastructure.Persistence.SqlServer;
using Wally.RomMaster.HashService.Infrastructure.PipelineBehaviours;
using Wally.RomMaster.HashService.Tests.ConventionTests.Helpers;
using Wally.RomMaster.HashService.WebApi;

namespace Wally.RomMaster.HashService.Tests.ConventionTests;

public static class Configuration
{
	public static Types Types => new() { AppSettings = new[] { typeof(AppSettings), }, };

	public static Assemblies Assemblies =>
		new()
		{
			Application =
				new[]
				{
					typeof(IApplicationAssemblyMarker).Assembly,
					typeof(IApplicationContractsAssemblyMarker).Assembly,
					typeof(IApplicationMapperProfilesAssemblyMarker).Assembly,
					typeof(IApplicationMessagesAssemblyMarker).Assembly,
				},
			Domain = new[] { typeof(IDomainAssemblyMarker).Assembly, },
			Infrastructure = new[]
			{
				typeof(IInfrastructureDIMicrosoftAssemblyMarker).Assembly,
				typeof(IInfrastructureMessagingAssemblyMarker).Assembly,
				typeof(IInfrastructurePersistenceAssemblyMarker).Assembly,
				typeof(IInfrastructureSqlServerAssemblyMarker).Assembly,
				typeof(IInfrastructurePostgreSQLAssemblyMarker).Assembly,
				typeof(IInfrastructureMySqlAssemblyMarker).Assembly,
				typeof(IInfrastructurePipelineBehavioursAssemblyMarker).Assembly,
			},
			Presentation = new[] { typeof(IPresentationAssemblyMarker).Assembly, },
		};
}
