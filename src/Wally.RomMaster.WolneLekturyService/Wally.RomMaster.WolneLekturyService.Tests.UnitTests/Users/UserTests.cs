using System.Linq;
using FluentAssertions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;
using Xunit;

namespace Wally.RomMaster.WolneLekturyService.Tests.UnitTests.Users;

public class UserTests
{
	[Fact]
	public void Create_WithSpecifiedUserName_SetsIdAndName()
	{
		// Arrange
		User user;

		// Act
		user = User.Create("testUserName");

		// Assert
		user.Id.Should()
			.NotBeNull();
		user.Id.Value.Should()
			.NotBeEmpty();
		user.Name.Should()
			.NotBeNullOrWhiteSpace();
	}

	[Fact]
	public void Update_ForSpecifiedUser_UpdatesName()
	{
		// Arrange
		var id = new UserId();
		var user = User.Create(id, "testUserName");

		// Act
		user.Update("newTestName");

		// Assert
		user.Id.Should()
			.Be(id);
		user.Name.Should()
			.Be("newTestName");
	}

	[Fact]
	public void Create_ForNewDomainModel_ProducesDomainEvent()
	{
		// Arrange
		var id = new UserId();

		// Act
		var model = User.Create(id, "testUserName");

		// Assert
		model.GetDomainEvents()
			.Single()
			.Should()
			.BeOfType<UserCreatedDomainEvent>()
			.Subject.Id.Should()
			.Be(id);
	}
}
