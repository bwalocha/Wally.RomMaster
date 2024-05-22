using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.HashService.Domain.Users;
using Wally.RomMaster.HashService.Tests.IntegrationTests.Extensions;
using Wally.RomMaster.HashService.Tests.IntegrationTests.Helpers;
using Wally.RomMaster.HashService.WebApi;
using Xunit;

namespace Wally.RomMaster.HashService.Tests.IntegrationTests;

public partial class UsersControllerTests : IClassFixture<ApiWebApplicationFactory<Startup>>
{
	private readonly ApiWebApplicationFactory<Startup> _factory;
	
	private readonly HttpClient _httpClient;
	
	public UsersControllerTests(ApiWebApplicationFactory<Startup> factory)
	{
		_factory = factory;
		_httpClient = factory.CreateClient(
			new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false,
			});
		var database = factory.GetRequiredService<DbContext>();
		database.RemoveRange(database.Set<User>());
		database.SaveChanges();
	}
	
	private static User UserCreate(int index)
	{
		var userId = new UserId();
		var resource = User
			.Create($"testUser{index}")
			.SetCreatedById(userId);
		
		return resource;
	}
}
