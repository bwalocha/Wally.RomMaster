using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Application.Users;

public interface IUserRepository : IRepository<User, UserId>
{
}
