using Wally.RomMaster.HashService.Application.Abstractions;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Application.Users;

public interface IUserReadOnlyRepository : IReadOnlyRepository<User, UserId>
{
}
