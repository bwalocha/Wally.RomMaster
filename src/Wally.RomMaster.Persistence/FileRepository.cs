using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Wally.RomMaster.Application.Files;
using Wally.RomMaster.Domain.Files;
using Wally.RomMaster.Persistence.Abstractions;

namespace Wally.RomMaster.Persistence;

public class FileRepository : Repository<File>, IFileRepository
{
	public FileRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}

	public void RemoveOutdatedFiles(DateTime timestamp)
	{
		var toRemove = GetReadWriteEntitySet()
			.Where(a => a.ModifiedAt < timestamp);

		Remove(toRemove);
	}

	public Task<File?> GetOrDefaultAsync(FileLocation location, CancellationToken cancellationToken)
	{
		// Location is Unique so we can get First result
		return GetReadWriteEntitySet()
			.FirstOrDefaultAsync(a => a.Location.Location == location.Location, cancellationToken);
	}
}
