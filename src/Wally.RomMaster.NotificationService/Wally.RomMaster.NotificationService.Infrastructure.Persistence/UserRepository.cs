using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.NotificationService.Application.Users;
using Wally.RomMaster.NotificationService.Domain.Users;
using Wally.RomMaster.NotificationService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.NotificationService.Infrastructure.Persistence;

public class UserRepository : Repository<User, UserId>, IUserRepository
{
	public UserRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}
}
