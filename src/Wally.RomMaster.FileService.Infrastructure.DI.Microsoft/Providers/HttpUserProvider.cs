using System;

using Microsoft.AspNetCore.Http;

using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Providers;

public class HttpUserProvider : IUserProvider
{
	// private readonly IAccountRepository _accountRepository;
	private readonly IHttpContextAccessor _contextAccessor;

	public HttpUserProvider(IHttpContextAccessor contextAccessor /*, IAccountRepository accountRepository*/)
	{
		_contextAccessor = contextAccessor;

		// _accountRepository = accountRepository;
	}

	/*public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken)
	{
		var userIdClaim = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier) ??
						throw new UnauthorizedAccessException();
		var userId = Guid.Parse(userIdClaim.Value);
		// var userRole = _contextAccessor.HttpContext.User.FindAll(ClaimTypes.Role);
		// var account = await _accountRepository.GetAsync(userId, cancellationToken);
		var user = User.Create(userId, "HttpUser"/*userRole.Select(a => a.Value), account#1#);

		return user;
	}*/

	public Guid GetCurrentUserId()
	{
		throw new NotImplementedException();
	}
}
