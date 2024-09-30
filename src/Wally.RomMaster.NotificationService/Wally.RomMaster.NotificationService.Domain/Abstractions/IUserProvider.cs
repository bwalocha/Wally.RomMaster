using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Domain.Abstractions;

public interface IUserProvider
{
	UserId GetCurrentUserId();
}
