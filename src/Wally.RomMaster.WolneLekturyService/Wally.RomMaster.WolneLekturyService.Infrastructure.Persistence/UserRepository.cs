using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.WolneLekturyService.Application.Users;
using Wally.RomMaster.WolneLekturyService.Domain.Users;
using Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence;

public class UserRepository : Repository<User, UserId>, IUserRepository
{
	public UserRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}
}
