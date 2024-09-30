using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Application.Users;

public interface IUserReadOnlyRepository : IReadOnlyRepository<User, UserId>
{
}
