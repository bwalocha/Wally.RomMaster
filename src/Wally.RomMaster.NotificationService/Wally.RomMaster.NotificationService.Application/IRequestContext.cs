using Wally.RomMaster.NotificationService.Domain;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Application;

public interface IRequestContext
{
	public CorrelationId CorrelationId { get; }

	public UserId UserId { get; }
}
