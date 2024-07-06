using Wally.RomMaster.HashService.Domain;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Application;

public interface IRequestContext
{
	public CorrelationId CorrelationId { get; }

	public UserId UserId { get; }
}
