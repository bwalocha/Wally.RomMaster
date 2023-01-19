using Wally.RomMaster.FileService.Application.Users.Commands;
using Wally.RomMaster.FileService.Contracts.Requests.Users;
using Wally.RomMaster.FileService.ConventionTests.Helpers;
using Wally.RomMaster.FileService.Domain.Users;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.FileService.MapperProfiles;
using Wally.RomMaster.FileService.Messaging.Consumers;
using Wally.RomMaster.FileService.Persistence;
using Wally.RomMaster.FileService.Persistence.SqlServer;
using Wally.RomMaster.FileService.PipelineBehaviours;
using Wally.RomMaster.FileService.WebApi;

namespace Wally.RomMaster.FileService.ConventionTests;

public static class Configuration
{
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
