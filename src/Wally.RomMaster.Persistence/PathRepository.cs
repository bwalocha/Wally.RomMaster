using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Wally.RomMaster.Application.Paths;
using Wally.RomMaster.Domain.Files;
using Wally.RomMaster.Persistence.Abstractions;

namespace Wally.RomMaster.Persistence;

public class PathRepository : Repository<Path>, IPathRepository
{
	public PathRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}

	public Task<Path?> GetOrDefaultAsync(FileLocation location, CancellationToken cancellationToken)
	{
		return GetReadWriteEntitySet()
			.SingleOrDefaultAsync(a => a.Name == location.Location.LocalPath, cancellationToken);
	}
}
