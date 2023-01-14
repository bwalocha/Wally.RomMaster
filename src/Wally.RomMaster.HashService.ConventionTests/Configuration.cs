using Wally.RomMaster.HashService.Application.Users.Commands;
using Wally.RomMaster.HashService.Contracts.Requests.Users;
using Wally.RomMaster.HashService.ConventionTests.Helpers;
using Wally.RomMaster.HashService.Domain.Users;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.HashService.MapperProfiles;
using Wally.RomMaster.HashService.Messaging.Consumers;
using Wally.RomMaster.HashService.Persistence;
using Wally.RomMaster.HashService.Persistence.SqlServer;
using Wally.RomMaster.HashService.PipelineBehaviours;
using Wally.RomMaster.HashService.WebApi;

namespace Wally.RomMaster.HashService.ConventionTests;

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
				typeof(ServiceCollectionExtensions).Assembly, typeof(UserCreatedConsumer).Assembly,
				typeof(ApplicationDbContext).Assembly, typeof(Helper).Assembly, typeof(LogBehavior<,>).Assembly,
			},
			Presentation = new[] { typeof(Startup).Assembly, },
		};
}
