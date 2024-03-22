﻿using System.Linq;
using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Execution;
using FluentAssertions.Types;
using Wally.RomMaster.HashService.Application.Contracts;
using Wally.RomMaster.HashService.Tests.ConventionTests.Extensions;
using Wally.RomMaster.HashService.Tests.ConventionTests.Helpers;
using Wally.Lib.DDD.Abstractions.Requests;
using Xunit;

namespace Wally.RomMaster.HashService.Tests.ConventionTests;

public class RequestTests
{
	[Fact]
	public void Application_Request_ShouldNotExposeSetter()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var assembly in assemblies)
			{
				var types = AllTypes.From(assembly)
					.ThatImplement<IRequest>();

				foreach (var type in types)
				{
					foreach (var property in type.Properties())
					{
						if (property.SetMethod?.IsPublic != true)
						{
							continue;
						}

						property.Should()
							.BeWritable(
								CSharpAccessModifier.Private,
								"Response class '{0}' should not expose setter '{1}'",
								type,
								property);
					}
				}
			}
		}
	}

	[Fact]
	public void Application_ClassesWhichImplementsIRequest_ShouldBeInTheSameProject()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var applicationNamespace = $"{typeof(IApplicationContractsAssemblyMarker).Namespace}.Requests";

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var assembly in assemblies)
			{
				var types = AllTypes.From(assembly);

				types.ThatImplement<IRequest>()
					.Should()
					.BeUnderNamespace(applicationNamespace);
			}
		}
	}

	[Fact]
	public void Application_AllClassesEndsWithRequest_ShouldImplementIRequest()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		using (new AssertionScope())
		{
			foreach (var type in types.Where(a => a.Name.EndsWith("Request") && a.Name != nameof(IRequest)))
			{
				type.Should()
					.Implement<IRequest>();
			}
		}
	}

	[Fact]
	public void Application_AllClassesImplementsIRequest_ShouldHaveRequestSuffix()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		using (new AssertionScope())
		{
			foreach (var type in types.ThatImplement<IRequest>())
			{
				type.Name.Should()
					.EndWith("Request");
			}
		}
	}

	[Fact]
	public void Application_AllClassesImplementsIRequest_ShouldHaveCorrespondingValidator()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var assembly in assemblies)
			{
				foreach (var type in assembly.GetTypes()
							.ThatImplement<IRequest>())
				{
					assemblies.SelectMany(a => a.GetTypes())
						.SingleOrDefault(a => a.Name == $"{type.Name}Validator")
						.Should()
						.NotBeNull("every Request '{0}' should have corresponding Validator", type);
				}
			}
		}
	}
}
