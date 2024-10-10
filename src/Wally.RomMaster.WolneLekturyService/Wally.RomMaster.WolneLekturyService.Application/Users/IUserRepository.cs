using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Application.Users;

public interface IUserRepository : IRepository<User, UserId>
{
}