using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.WolneLekturyService.Domain.Users;
using Wally.RomMaster.WolneLekturyService.Tests.IntegrationTests.Extensions;
using Wally.RomMaster.WolneLekturyService.Tests.IntegrationTests.Helpers;
using Wally.RomMaster.WolneLekturyService.WebApi;
using Xunit;

namespace Wally.RomMaster.WolneLekturyService.Tests.IntegrationTests;

public partial class UsersControllerTests : IClassFixture<ApiWebApplicationFactory<Startup>>, IDisposable
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
	}

	private static User UserCreate(int index)
	{
		var userId = new UserId();
		var resource = User
			.Create($"testUser{index}")
			.SetCreatedById(userId);

		return resource;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	
	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			_factory.RemoveAll<User>();
			_httpClient.Dispose();
		}
	}
}
