using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Application.Users;

public interface IUserReadOnlyRepository : IReadOnlyRepository<User, UserId>
{
}
