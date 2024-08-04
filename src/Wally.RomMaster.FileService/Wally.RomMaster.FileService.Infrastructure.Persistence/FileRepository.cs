using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.FileService.Application.Files;
using Wally.RomMaster.FileService.Domain.Files;
using Wally.RomMaster.FileService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.FileService.Infrastructure.Persistence;

public class FileRepository : Repository<File, FileId>, IFileRepository
{
	public FileRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}

	public void RemoveOutdatedFiles(DateTime timestamp)
	{
		var entities = GetReadWriteEntitySet()
			.Where(a => a.ModifiedAt < timestamp);

		foreach (var entity in entities)
		{
			Remove(entity);
		}
	}

	public Task<File?> GetOrDefaultAsync(FileLocation location, CancellationToken cancellationToken)
	{
		// Location is Unique so we can get First result
		return GetReadWriteEntitySet()
			.FirstOrDefaultAsync(a => a.Location.Value == location.Value, cancellationToken);
	}
	
	public Task<File?> GetOrDefaultAsync(FileId id, CancellationToken cancellationToken)
	{
		// Id is Unique so we can get First result
		return GetReadWriteEntitySet()
			.FirstOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
	}
}
