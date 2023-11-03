using System.Threading;
using System.Threading.Tasks;

using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Paths;

public interface IPathRepository : IRepository<Path, PathId>
{
	Task<Path?> GetOrDefaultAsync(FileLocation location, CancellationToken cancellationToken);
}
