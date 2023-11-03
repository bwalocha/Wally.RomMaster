using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;

using Wally.RomMaster.FileService.Tests.ConventionTests.Helpers;
using Wally.Lib.DDD.Abstractions.Commands;

using Xunit;

namespace Wally.RomMaster.FileService.Tests.ConventionTests;

public class CommandHandlerTests
{
	[Fact]
	public void Application_AllClassesEndsWithCommandHandler_ShouldImplementICommandHandler()
	{
		var applicationTypes = Configuration.Assemblies.Application.GetAllTypes();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var type in applicationTypes.Where(a => a.Name.EndsWith("CommandHandler")))
			{
				type.Should()
					.BeAssignableTo(
						typeof(ICommandHandler<>),
						"All command handlers should implement ICommandHandler interface");

				// TODO: add tests for ICommandHandler<,>
				/*type.Should()
					.BeAssignableTo(
						typeof(ICommandHandler<,>),
						"All command handlers should implement ICommandHandler interface");*/
			}
		}
	}
}
