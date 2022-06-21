using System.Threading;
using System.Threading.Tasks;

using Wally.RomMaster.Domain.DataFiles;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Domain.Abstractions;

public interface IDataFileParser
{
	Task<DataFile> ParseAsync(FileLocation location, CancellationToken cancellationToken);
}
