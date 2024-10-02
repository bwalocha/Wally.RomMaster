using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

public interface IUserProvider
{
	UserId GetCurrentUserId();
}
