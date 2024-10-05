using Wally.RomMaster.NotificationService.Application;
using Wally.RomMaster.NotificationService.Application.Contracts;
using Wally.RomMaster.NotificationService.Application.MapperProfiles;
using Wally.RomMaster.NotificationService.Application.Messages;
using Wally.RomMaster.NotificationService.Domain;
using Wally.RomMaster.NotificationService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.NotificationService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.NotificationService.Infrastructure.Messaging;
using Wally.RomMaster.NotificationService.Infrastructure.Persistence;
using Wally.RomMaster.NotificationService.Infrastructure.Persistence.MySql;
using Wally.RomMaster.NotificationService.Infrastructure.Persistence.PostgreSQL;
using Wally.RomMaster.NotificationService.Infrastructure.Persistence.SqlServer;
using Wally.RomMaster.NotificationService.Infrastructure.PipelineBehaviours;
using Wally.RomMaster.NotificationService.Tests.ConventionTests.Helpers;
using Wally.RomMaster.NotificationService.WebApi;

namespace Wally.RomMaster.NotificationService.Tests.ConventionTests;

public static class Configuration
{
	public const string Namespace = "Wally.RomMaster.NotificationService";

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
				// typeof(IApplicationMessagesAssemblyMarker).Assembly,
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
