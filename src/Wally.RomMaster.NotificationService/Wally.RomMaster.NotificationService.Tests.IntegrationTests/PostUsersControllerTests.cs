using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.NotificationService.Application.Contracts.Users.Requests;
using Wally.RomMaster.NotificationService.Domain.Users;
using Wally.RomMaster.NotificationService.Tests.IntegrationTests.Extensions;
using Xunit;

namespace Wally.RomMaster.NotificationService.Tests.IntegrationTests;

public partial class UsersControllerTests
{
	[Fact(Skip = "No Database")]
	public async Task Post_ForNewResource_CreatesNewResource()
	{
		// Arrange
		var request = new CreateUserRequest("newName3");

		// Act
		var response = await _httpClient.PostAsync("Users", request, CancellationToken.None);

		// Assert
		response.IsSuccessStatusCode.Should()
			.BeTrue();
		response.StatusCode.Should()
			.Be(HttpStatusCode.OK);
		(await _factory.GetRequiredService<DbContext>()
				.Set<User>()
				.SingleAsync())
			.Name.Should()
			.Be("newName3");
	}

	[Fact(Skip = "No Database")]
	public async Task Post_ForInvalidRequest_ReturnsBadRequest()
	{
		// Arrange
		var request = new CreateUserRequest(string.Empty);

		// Act
		var response = await _httpClient.PostAsync("Users", request, CancellationToken.None);

		// Assert
		response.IsSuccessStatusCode.Should()
			.BeFalse();
		response.StatusCode.Should()
			.Be(HttpStatusCode.BadRequest);
		_factory.GetRequiredService<DbContext>()
			.Set<User>()
			.Should()
			.BeEmpty();
	}
}
