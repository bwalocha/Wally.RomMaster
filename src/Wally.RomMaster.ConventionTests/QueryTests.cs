using System.Diagnostics.CodeAnalysis;
using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Types;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.Application.Users.Queries;
using Wally.RomMaster.ConventionTests.Helpers;

using Xunit;

namespace Wally.RomMaster.ConventionTests;

public class QueryTests
{
	[Fact]
	public void Application_Query_ShouldNotExposeSetter()
	{
		var applicationTypes = AllTypes.From(typeof(GetUserQuery).Assembly);

		applicationTypes.Where(a => typeof(IQuery<>).IsAssignableFrom(a))
			.Types()
			.Properties()
			.Should()
			.NotBeWritable("query should be immutable");
	}

	[Fact]
	public void Application_Query_ShouldBeExcludedFromCodeCoverage()
	{
		var applicationTypes = AllTypes.From(typeof(GetUserQuery).Assembly);

		applicationTypes.Where(a => a.ImplementsGenericInterface(typeof(IQuery<>)))
			.Types()
			.Should()
			.BeDecoratedWith<ExcludeFromCodeCoverageAttribute>();
	}

	[Fact]
	public void Application_Query_ShouldBeSealed()
	{
		var applicationTypes = AllTypes.From(typeof(GetUserQuery).Assembly);

		applicationTypes.Where(a => typeof(IQuery<>).IsAssignableFrom(a))
			.Types()
			.Should()
			.BeSealed();
	}

	[Fact]
	public void Application_Query_ShouldHaveCorrespondingHandler()
	{
		var assemblies = TypeHelpers.GetAllInternalAssemblies();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var assembly in assemblies)
			{
				foreach (var type in assembly.GetTypes()
							.Where(a => a.ImplementsGenericInterface(typeof(IQuery<>)))
							.Types())
				{
					assemblies.SelectMany(a => a.GetTypes())
						.SingleOrDefault(a => a.Name == $"{type.Name}Handler")
						.Should()
						.NotBeNull("Query '{0}' should have corresponding Handler", type);
				}
			}
		}
	}
}
