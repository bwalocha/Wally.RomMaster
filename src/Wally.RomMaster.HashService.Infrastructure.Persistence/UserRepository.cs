using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Wally.RomMaster.HashService.Application.Users;
using Wally.RomMaster.HashService.Domain.Users;
using Wally.RomMaster.HashService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.HashService.Infrastructure.Persistence;

public class UserRepository : Repository<User>, IUserRepository
{
	public UserRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}
}
