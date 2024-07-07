using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.HashService.Application.Files;
using Wally.RomMaster.HashService.Domain.Files;
using Wally.RomMaster.HashService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.HashService.Infrastructure.Persistence;

public class FileRepository : Repository<File, FileId>, IFileRepository
{
	public FileRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}

	public Task<File?> GetOrDefaultAsync(FileId id, CancellationToken cancellationToken)
	{
		return GetReadWriteEntitySet()
			.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
	}
}
