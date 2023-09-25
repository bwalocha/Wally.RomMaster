using System.Diagnostics.CodeAnalysis;
using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.FileService.Tests.ConventionTests.Helpers;

using Xunit;

namespace Wally.RomMaster.FileService.Tests.ConventionTests;

public class QueryTests
{
	[Fact]
	public void Application_Query_ShouldNotExposeSetter()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		types.Where(a => typeof(IQuery<>).IsAssignableFrom(a))
			.Types()
			.Properties()
			.Should()
			.NotBeWritable("query should be immutable");
	}

	[Fact]
	public void Application_Query_ShouldBeExcludedFromCodeCoverage()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		types.Where(a => a.ImplementsGenericInterface(typeof(IQuery<>)))
			.Types()
			.Should()
			.BeDecoratedWith<ExcludeFromCodeCoverageAttribute>();
	}

	[Fact]
	public void Application_Query_ShouldBeSealed()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		types.Where(a => a.ImplementsGenericInterface(typeof(IQuery<>)))
			.Types()
			.Should()
			.BeSealed();
	}

	[Fact]
	public void Application_Query_ShouldHaveCorrespondingHandler()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();

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
