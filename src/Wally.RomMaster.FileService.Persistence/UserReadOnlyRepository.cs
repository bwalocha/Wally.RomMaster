using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Wally.RomMaster.FileService.Application.Users;
using Wally.RomMaster.FileService.Domain.Users;
using Wally.RomMaster.FileService.Persistence.Abstractions;

namespace Wally.RomMaster.FileService.Persistence;

public class UserReadOnlyRepository : ReadOnlyRepository<User>, IUserReadOnlyRepository
{
	public UserReadOnlyRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}
}
