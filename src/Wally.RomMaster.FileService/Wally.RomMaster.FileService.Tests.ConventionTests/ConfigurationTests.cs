using System.Linq;
using FluentAssertions;
using Wally.RomMaster.FileService.Tests.ConventionTests.Helpers;
using Xunit;

namespace Wally.RomMaster.FileService.Tests.ConventionTests;

public class ConfigurationTests
{
	[Fact]
	public void Configuration_ShouldContainsAllAssemblies()
	{
		// Arrange
		var fromConfig = Configuration.Assemblies.GetAllAssemblies()
			.ToList();
		var fromInternal = TypeHelpers.GetAllInternalAssemblies()
			.ToList();

		// Act

		// Assert
		fromConfig.Should()
			.AllSatisfy(a => fromInternal.Contains(a));
	}
}
