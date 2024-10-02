using Wally.RomMaster.WolneLekturyService.Domain;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Application;

public interface IRequestContext
{
	public CorrelationId CorrelationId { get; }

	public UserId UserId { get; }
}
