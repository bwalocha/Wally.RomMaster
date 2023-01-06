using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Wally.RomMaster.FileService.Application.Users;
using Wally.RomMaster.FileService.Domain.Users;
using Wally.RomMaster.FileService.Persistence.Abstractions;

namespace Wally.RomMaster.FileService.Persistence;

public class UserRepository : Repository<User>, IUserRepository
{
	public UserRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}
}
