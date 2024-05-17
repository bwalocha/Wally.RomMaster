using System;
using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files;

public interface IFileRepository : IRepository<File, FileId>
{
	void RemoveOutdatedFiles(DateTime timestamp);

	Task<File?> GetOrDefaultAsync(FileLocation location, CancellationToken cancellationToken);
}
