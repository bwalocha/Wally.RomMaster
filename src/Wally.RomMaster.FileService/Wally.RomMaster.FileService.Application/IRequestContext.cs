using Wally.RomMaster.FileService.Domain;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Application;

public interface IRequestContext
{
	public CorrelationId CorrelationId { get; }

	public UserId UserId { get; }
}
