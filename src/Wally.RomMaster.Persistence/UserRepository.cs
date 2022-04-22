using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Wally.RomMaster.Application.Users;
using Wally.RomMaster.Domain.Users;
using Wally.RomMaster.Persistence.Abstractions;

namespace Wally.RomMaster.Persistence;

public class UserRepository : Repository<User>, IUserRepository
{
	public UserRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}
}
