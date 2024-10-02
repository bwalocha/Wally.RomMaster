using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Tests.ConventionTests.Extensions;
using Wally.RomMaster.WolneLekturyService.Tests.ConventionTests.Helpers;
using Xunit;

namespace Wally.RomMaster.WolneLekturyService.Tests.ConventionTests;

public class QueryHandlerTests
{
	[Fact]
	public void Application_AllClassesEndsWithQueryHandler_ShouldImplementIQueryHandler()
	{
		var applicationTypes = Configuration.Assemblies.Application.GetAllTypes();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var type in applicationTypes.Where(a => a.Name.EndsWith("QueryHandler")))
			{
				type.Should()
					.BeAssignableTo(
						typeof(IQueryHandler<,>),
						"All query handlers should implement IQueryHandler interface");
			}
		}
	}

	[Fact]
	public void Application_AllClassesImplementedIQueryHandler_ShouldEndsWithQueryHandler()
	{
		var applicationTypes = Configuration.Assemblies.Application.GetAllTypes();
		var handlerTypes = applicationTypes
			.Where(a => a.IsClass)
			.Where(a => !a.IsAbstract)
			.Where(
				a => a.ImplementsGenericInterface(typeof(IQueryHandler<,>)));

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var type in handlerTypes)
			{
				type.Name
					.Should()
					.EndWith("QueryHandler", "All query handlers name should ends with 'QueryHandler'");
			}
		}
	}
}
