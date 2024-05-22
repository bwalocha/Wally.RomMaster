using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.HashService.Application.Contracts.Requests.Users;
using Wally.RomMaster.HashService.Domain.Users;
using Wally.RomMaster.HashService.Tests.IntegrationTests.Extensions;
using Xunit;

namespace Wally.RomMaster.HashService.Tests.IntegrationTests;

public partial class UsersControllerTests
{
	[Fact(Skip = "No persistence")]
	public async Task Put_ForExistingResource_UpdatesResourceData()
	{
		// Arrange
		var resource = UserCreate(3);
		await _factory.SeedAsync(resource);
		var request = new UpdateUserRequest("newTestResource1");
		
		// Act
		var response = await _httpClient.PutAsync($"Users/{resource.Id.Value}", request, CancellationToken.None);
		
		// Assert
		response.IsSuccessStatusCode.Should()
			.BeTrue();
		response.StatusCode.Should()
			.Be(HttpStatusCode.OK);
		(await _factory.GetRequiredService<DbContext>()
			.Set<User>()
			.SingleAsync(a => a.Id == resource.Id))
			.Name.Should()
			.Be("newTestResource1");
	}
}
