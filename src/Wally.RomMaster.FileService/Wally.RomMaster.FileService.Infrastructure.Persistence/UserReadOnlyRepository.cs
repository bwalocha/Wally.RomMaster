using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.FileService.Application.Users;
using Wally.RomMaster.FileService.Domain.Users;
using Wally.RomMaster.FileService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.FileService.Infrastructure.Persistence;

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
