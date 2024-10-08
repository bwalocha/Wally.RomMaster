using FluentAssertions;
using FluentValidation;
using Wally.RomMaster.NotificationService.Application.Contracts.Users.Requests;
using Xunit;

namespace Wally.RomMaster.NotificationService.Tests.UnitTests.Users;

// https://docs.fluentvalidation.net/en/latest/testing.html
public class GetUsersRequestValidatorTests
{
	private readonly AbstractValidator<GetUsersRequest> _validator;

	public GetUsersRequestValidatorTests()
	{
		_validator = new GetUsersRequestValidator();
	}

	[Fact]
	public void Validate_ForValidData_IsValid()
	{
		// Arrange
		var instance = new GetUsersRequest();

		// Act
		var result = _validator.Validate(instance);

		// Assert
		result.IsValid.Should()
			.BeTrue();
	}
}
