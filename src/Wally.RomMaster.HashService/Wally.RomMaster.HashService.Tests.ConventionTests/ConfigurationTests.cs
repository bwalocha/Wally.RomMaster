﻿using System.Linq;
using FluentAssertions;
using Wally.RomMaster.HashService.Tests.ConventionTests.Helpers;
using Xunit;

namespace Wally.RomMaster.HashService.Tests.ConventionTests;

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
			.AllSatisfy(a => fromInternal.Should()
				.Contain(a));
	}
}
