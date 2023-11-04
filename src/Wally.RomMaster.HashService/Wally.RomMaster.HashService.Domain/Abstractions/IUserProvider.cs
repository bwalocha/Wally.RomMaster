using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public interface IUserProvider
{
	UserId GetCurrentUserId();
}
