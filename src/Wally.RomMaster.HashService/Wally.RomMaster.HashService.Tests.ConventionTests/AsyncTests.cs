﻿using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Wally.RomMaster.HashService.Tests.ConventionTests.Extensions;
using Wally.RomMaster.HashService.Tests.ConventionTests.Helpers;
using Xunit;

namespace Wally.RomMaster.HashService.Tests.ConventionTests;

public class AsyncTests
{
	[Fact]
	public void AsyncMethods_ShouldHaveCancellationTokenAsLastParam()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllExportedTypes();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var type in types)
			{
				foreach (var method in type.Methods()
							.Where(
								a => a.ReturnType == typeof(Task) ||
									a.ReturnType.InheritsGenericClass(typeof(Task<>)) ||
									a.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null))
				{
					if (method.Name.StartsWith("<>"))
					{
						// skip testing AutoGenerated method
						continue;
					}

					if (type.ImplementsGenericInterface(typeof(IConsumer<>)) &&
						method.Name == nameof(IConsumer<object>.Consume))
					{
						continue;
					}

					if (type.InheritsGenericClass(typeof(Hub<>)) && method.Name == nameof(Hub<object>.OnConnectedAsync))
					{
						continue;
					}

					if (type.InheritsGenericClass(typeof(Hub<>)) &&
						method.Name == nameof(Hub<object>.OnDisconnectedAsync))
					{
						continue;
					}

					var parameters = method.GetParameters();

					parameters.LastOrDefault()
						?.ParameterType.Should()
						.Be<CancellationToken>(
							"Method '{0}' should contain cancellation token as the last param in type '{1}'",
							method,
							type);
				}
			}
		}
	}

	[Fact]
	public void AsyncMethods_ShouldHaveAsyncSuffix()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllExportedTypes();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var type in types)
			{
				foreach (var method in type.Methods()
							.Where(
								a => a.ReturnType == typeof(Task) ||
									a.ReturnType.InheritsGenericClass(typeof(Task<>)) ||
									a.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null))
				{
					if (method.Name.StartsWith("<"))
					{
						// skip testing AutoGenerated method
						continue;
					}

					if (method.GetBaseDefinition()
							.DeclaringType != method.DeclaringType)
					{
						continue;
					}

					if (type.ImplementsGenericInterface(typeof(IPipelineBehavior<,>)) &&
						method.Name == nameof(IPipelineBehavior<IRequest<object>, object>.Handle))
					{
						continue;
					}

					if (type.ImplementsGenericInterface(typeof(IConsumer<>)) &&
						method.Name == nameof(IConsumer<object>.Consume))
					{
						continue;
					}

					method.Name.Should()
						.EndWith("Async", "Method '{0}' in type '{1}' should contain Async suffix ", method, type);
				}
			}
		}
	}
}
