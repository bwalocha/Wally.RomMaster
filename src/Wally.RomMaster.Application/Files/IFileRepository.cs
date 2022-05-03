using System;
using System.Threading;
using System.Threading.Tasks;

using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Application.Files;

public interface IFileRepository : IRepository<File>
{
	void RemoveOutdatedFiles(DateTime timestamp);

	Task<File?> GetOrDefaultAsync(FileLocation location, CancellationToken cancellationToken);
}
