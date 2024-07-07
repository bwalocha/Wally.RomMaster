using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.HashService.Application.Files;
using Wally.RomMaster.HashService.Domain.Files;
using Wally.RomMaster.HashService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.HashService.Infrastructure.Persistence;

public class FileReadOnlyRepository : ReadOnlyRepository<File, FileId>, IFileReadOnlyRepository
{
	public FileReadOnlyRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}
}
