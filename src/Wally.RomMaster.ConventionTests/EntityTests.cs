using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Types;

using Wally.Lib.DDD.Abstractions.DomainModels;
using Wally.RomMaster.ConventionTests.Helpers;
using Wally.RomMaster.Domain.Users;

using Xunit;

namespace Wally.RomMaster.ConventionTests;

public class EntityTests
{
	[Fact]
	public void Entity_Constructor_ShouldBePrivate()
	{
		var applicationTypes = AllTypes.From(typeof(User).Assembly);

		using (new AssertionScope(new AssertionStrategy()))
		{
			applicationTypes.ThatImplement<Entity>()
				.Should()
				.HaveOnlyPrivateParameterlessConstructor();
		}
	}
}
