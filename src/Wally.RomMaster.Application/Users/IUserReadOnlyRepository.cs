using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.Users;

namespace Wally.RomMaster.Application.Users;

public interface IUserReadOnlyRepository : IReadOnlyRepository<User>
{
}
