using System;
using System.Threading;
using System.Threading.Tasks;

using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files;

public interface IFileRepository : IRepository<File>
{
	void RemoveOutdatedFiles(DateTime timestamp);

	Task<File?> GetOrDefaultAsync(FileLocation location, CancellationToken cancellationToken);
}
