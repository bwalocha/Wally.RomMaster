using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using MassTransit;
using Wally.RomMaster.NotificationService.Tests.ConventionTests.Extensions;
using Xunit;

namespace Wally.RomMaster.NotificationService.Tests.ConventionTests;

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
					.BeAssignableTo(typeof(IConsumer<>));
			}
		}
	}

	[Fact]
	public void Infrastructure_AllClassesInheritsConsumer_ShouldHaveConsumerSuffix()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		using (new AssertionScope())
		{
			foreach (var type in types.Where(a => !a.Name.EndsWith("FaultConsumer"))
						.Where(a => a.ImplementsGenericInterface(typeof(IConsumer<>))))
			{
				type.Name.Should()
					.EndWith("MessageConsumer");
			}
		}
	}

	[Fact]
	public void Infrastructure_AllClassesInheritsConsumer_ShouldHaveMessagePrefix()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();
		var types = assemblies.GetAllTypes();

		using (new AssertionScope())
		{
			foreach (var type in types.Where(a => !a.Name.EndsWith("FaultConsumer"))
						.Where(a => a.ImplementsGenericInterface(typeof(IConsumer<>))))
			{
				var genericType = type.GetGenericInterface(typeof(IConsumer<>)) !.GenericTypeArguments.Single();

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
