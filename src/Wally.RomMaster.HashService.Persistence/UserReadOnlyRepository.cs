using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Wally.RomMaster.HashService.Application.Users;
using Wally.RomMaster.HashService.Domain.Users;
using Wally.RomMaster.HashService.Persistence.Abstractions;

namespace Wally.RomMaster.HashService.Persistence;

public class UserReadOnlyRepository : ReadOnlyRepository<User>, IUserReadOnlyRepository
{
	public UserReadOnlyRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}
}
