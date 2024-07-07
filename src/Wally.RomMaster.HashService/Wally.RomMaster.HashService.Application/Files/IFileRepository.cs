using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.HashService.Application.Abstractions;
using Wally.RomMaster.HashService.Domain.Files;

namespace Wally.RomMaster.HashService.Application.Files;

public interface IFileRepository : IRepository<File, FileId>
{
	Task<File?> GetOrDefaultAsync(FileId id, CancellationToken cancellationToken);
}
