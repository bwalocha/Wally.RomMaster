using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.WolneLekturyService.Application.Users;
using Wally.RomMaster.WolneLekturyService.Domain.Users;
using Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence;

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
