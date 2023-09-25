using Wally.RomMaster.HashService.Domain.Abstractions;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Application.Users;

public interface IUserReadOnlyRepository : IReadOnlyRepository<User>
{
}
