using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Wally.RomMaster.FileService.Application.Paths;
using Wally.RomMaster.FileService.Domain.Files;
using Wally.RomMaster.FileService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.FileService.Infrastructure.Persistence;

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
