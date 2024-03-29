﻿using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Wally.RomMaster.FileService.Tests.ConventionTests.Helpers;
using Wally.Lib.DDD.Abstractions.Commands;
using Xunit;

namespace Wally.RomMaster.FileService.Tests.ConventionTests;

public class CommandTests
{
	[Fact]
	public void Application_Command_ShouldNotExposeSetter()
	{
		var applicationTypes = Configuration.Assemblies.Application.GetAllTypes();

		applicationTypes.ThatImplement<ICommand>()
			.Properties()
			.Should()
			.NotBeWritable("commands should be immutable");
	}

	[Fact]
	public void Application_Command_ShouldBeExcludedFromCodeCoverage()
	{
		var applicationTypes = Configuration.Assemblies.Application.GetAllTypes();

		applicationTypes.ThatImplement<ICommand>()
			.Should()
			.BeDecoratedWith<ExcludeFromCodeCoverageAttribute>();
	}

	[Fact]
	public void Application_Command_ShouldHaveCorrespondingHandler()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();

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
		var assemblies = Configuration.Assemblies.GetAllAssemblies();

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

	[Fact]
	public void Application_Command_ShouldBeSealed()
	{
		var applicationTypes = Configuration.Assemblies.Application.GetAllTypes();

		applicationTypes.ThatImplement<ICommand>()
			.ThatAreNotSealed()
			.Should()
			.BeSealed("commands should be sealed");
	}
}
