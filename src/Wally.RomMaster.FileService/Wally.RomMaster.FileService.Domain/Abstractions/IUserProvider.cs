using System;

namespace Wally.RomMaster.FileService.Domain.Abstractions;

public interface IUserProvider
{
	Guid GetCurrentUserId();
}
