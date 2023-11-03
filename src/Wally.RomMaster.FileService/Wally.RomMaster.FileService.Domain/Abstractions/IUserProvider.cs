using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Domain.Abstractions;

public interface IUserProvider
{
	UserId GetCurrentUserId();
}
