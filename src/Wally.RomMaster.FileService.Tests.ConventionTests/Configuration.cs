using Wally.RomMaster.FileService.Application.Contracts.Requests.Users;
using Wally.RomMaster.FileService.Application.MapperProfiles;
using Wally.RomMaster.FileService.Application.Users.Commands;
using Wally.RomMaster.FileService.Domain.Users;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.FileService.Infrastructure.Messaging.Consumers;
using Wally.RomMaster.FileService.Infrastructure.Persistence;
using Wally.RomMaster.FileService.Infrastructure.Persistence.SqlServer;
using Wally.RomMaster.FileService.Infrastructure.PipelineBehaviours;
using Wally.RomMaster.FileService.Tests.ConventionTests.Helpers;
using Wally.RomMaster.FileService.WebApi;

namespace Wally.RomMaster.FileService.Tests.ConventionTests;

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
