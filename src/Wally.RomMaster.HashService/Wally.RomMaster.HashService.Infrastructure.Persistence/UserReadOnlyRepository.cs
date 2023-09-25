using System.Linq;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Wally.RomMaster.HashService.Application.Users;
using Wally.RomMaster.HashService.Domain.Users;
using Wally.RomMaster.HashService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.HashService.Infrastructure.Persistence;

public class UserReadOnlyRepository : ReadOnlyRepository<User>, IUserReadOnlyRepository
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
