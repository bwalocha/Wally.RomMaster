using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.NotificationService.Application.Users;
using Wally.RomMaster.NotificationService.Domain.Users;
using Wally.RomMaster.NotificationService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.NotificationService.Infrastructure.Persistence;

public class UserReadOnlyRepository : ReadOnlyRepository<User, UserId>, IUserReadOnlyRepository
{
	public UserReadOnlyRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}

	protected override IQueryable<User> ApplySearch(IQueryable<User> query, string term)
	{
		return query.Where(a => a.Name.StartsWith(term));
	}
}
