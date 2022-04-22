using System.Diagnostics.CodeAnalysis;
using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Types;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.Application.Users.Commands;
using Wally.RomMaster.ConventionTests.Helpers;

using Xunit;

namespace Wally.RomMaster.ConventionTests;

public class CommandTests
{
	[Fact]
	public void Application_Command_ShouldNotExposeSetter()
	{
		var applicationTypes = AllTypes.From(typeof(UpdateUserCommand).Assembly);

		applicationTypes.ThatImplement<ICommand>()
			.Properties()
			.Should()
			.NotBeWritable("commands should be immutable");
	}

	[Fact]
	public void Application_Command_ShouldBeExcludedFromCodeCoverage()
	{
		var applicationTypes = AllTypes.From(typeof(UpdateUserCommand).Assembly);

		applicationTypes.ThatImplement<ICommand>()
			.Should()
			.BeDecoratedWith<ExcludeFromCodeCoverageAttribute>();
	}

	[Fact]
	public void Application_Command_ShouldHaveCorrespondingHandler()
	{
		var assemblies = TypeHelpers.GetAllInternalAssemblies();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var assembly in assemblies)
			{
				foreach (var type in assembly.GetTypes()
							.ThatImplement<ICommand>())
				{
					assemblies.SelectMany(a => a.GetTypes())
						.SingleOrDefault(a => a.Name == $"{type.Name}Handler")
						.Should()
						.NotBeNull("Command '{0}' should have corresponding Handler", type);
				}
			}
		}
	}

	[Fact]
	public void Application_Command_ShouldHaveCorrespondingValidator()
	{
		var assemblies = TypeHelpers.GetAllInternalAssemblies();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var assembly in assemblies)
			{
				foreach (var type in assembly.GetTypes()
							.ThatImplement<ICommand>())
				{
					assemblies.SelectMany(a => a.GetTypes())
						.SingleOrDefault(a => a.Name == $"{type.Name}Validator")
						.Should()
						.NotBeNull("Command '{0}' should have corresponding Validator", type);
				}
			}
		}
	}
}
