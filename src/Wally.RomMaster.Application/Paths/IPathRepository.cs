using System.Threading;
using System.Threading.Tasks;

using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Application.Paths;

public interface IPathRepository : IRepository<Path>
{
	Task<Path?> GetOrDefaultAsync(FileLocation location, CancellationToken cancellationToken);
}
