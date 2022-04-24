using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;

using Wally.RomMaster.Application.Users.Queries;
using Wally.RomMaster.Domain.Users;
using Wally.RomMaster.MapperProfiles;
using Wally.RomMaster.Persistence;

using Xunit;

namespace Wally.RomMaster.ConventionTests;

public class OnionArchitectureTests
{
	[Fact]
	public void Domain_IsNotReferencedByApplication()
	{
		var domainTypes = new[] { typeof(User), };
		var applicationTypes = new[] { typeof(GetUserQueryHandler), };

		using (new AssertionScope())
		{
			domainTypes.Select(a => a.Assembly)
				.Should()
				.SatisfyRespectively(
					a =>
					{
						foreach (var type in applicationTypes)
						{
							a.Should()
								.NotReference(type.Assembly);
						}
					});
		}
	}

	[Fact]
	public void Domain_IsNotReferencedByInfrastructure()
	{
		var domainTypes = new[] { typeof(User), };
		var infrastructureTypes = new[] { typeof(UserProfile), };

		using (new AssertionScope())
		{
			domainTypes.Select(a => a.Assembly)
				.Should()
				.SatisfyRespectively(
					a =>
					{
						foreach (var type in infrastructureTypes)
						{
							a.Should()
								.NotReference(type.Assembly);
						}
					});
		}
	}

	[Fact]
	public void Domain_IsNotReferencedByPersistence()
	{
		var domainTypes = new[] { typeof(User), };
		var persistenceTypes = new[] { typeof(ApplicationDbContext), };

		using (new AssertionScope())
		{
			domainTypes.Select(a => a.Assembly)
				.Should()
				.SatisfyRespectively(
					a =>
					{
						foreach (var type in persistenceTypes)
						{
							a.Should()
								.NotReference(type.Assembly);
						}
					});
		}
	}
}
