using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;

using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.HashService.ConventionTests.Helpers;

using Xunit;

namespace Wally.RomMaster.HashService.ConventionTests;

public class ConsumerTests
{
	[Fact]
	public void Infrastructure_AllClassesEndsWithConsumer_ShouldInheritConsumer()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		using (new AssertionScope())
		{
			foreach (var type in types.Where(a => a.Name.EndsWith("Consumer")))
			{
				type.Should()
					.BeAssignableTo(typeof(Consumer<>));
			}
		}
	}

	[Fact]
	public void Application_AllClassesInheritsConsumer_ShouldHaveConsumerSuffix()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		using (new AssertionScope())
		{
			foreach (var type in types.Where(a => a.Name.EndsWith("Consumer"))
						.Where(a => a.InheritsGenericClass(typeof(Consumer<>))))
			{
				type.Name.Should()
					.EndWith("MessageConsumer");
			}
		}
	}

	[Fact]
	public void Application_AllClassesInheritsConsumer_ShouldHaveMessagePrefix()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		using (new AssertionScope())
		{
			foreach (var type in types.Where(a => a.Name.EndsWith("Consumer"))
						.Where(a => a.InheritsGenericClass(typeof(Consumer<>))))
			{
				var genericType = type.BaseType!.GenericTypeArguments.Single();

				type.Name.Should()
					.Be(
						$"{genericType.Name}Consumer",
						"Type '{0}' should have name '{1}'",
						type,
						$"{genericType.Name}Consumer");
			}
		}
	}
}
