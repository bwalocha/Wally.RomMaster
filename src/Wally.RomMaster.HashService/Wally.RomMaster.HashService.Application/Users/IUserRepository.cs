using Wally.RomMaster.HashService.Domain.Abstractions;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Application.Users;

public interface IUserRepository : IRepository<User, UserId>
{
}
