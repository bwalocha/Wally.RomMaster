using Wally.RomMaster.HashService.Application.Contracts.Requests.Users;
using Wally.RomMaster.HashService.Application.MapperProfiles;
using Wally.RomMaster.HashService.Application.Users.Commands;
using Wally.RomMaster.HashService.Domain.Users;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.HashService.Infrastructure.Messaging.Consumers;
using Wally.RomMaster.HashService.Infrastructure.Persistence;
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
					typeof(CreateUserCommand).Assembly, typeof(CreateUserRequest).Assembly,
					typeof(UserProfile).Assembly,
				},
			Domain = new[] { typeof(User).Assembly, },
			Infrastructure = new[]
			{
				typeof(ServiceCollectionExtensions).Assembly, typeof(UserCreatedMessageConsumer).Assembly,
				typeof(ApplicationDbContext).Assembly, typeof(Helper).Assembly, typeof(LogBehavior<,>).Assembly,
			},
			Presentation = new[] { typeof(Startup).Assembly, },
		};
}
