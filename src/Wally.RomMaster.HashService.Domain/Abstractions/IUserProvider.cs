using System;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public interface IUserProvider
{
	Guid GetCurrentUserId();
}
