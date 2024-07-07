using Wally.RomMaster.HashService.Application.Abstractions;
using Wally.RomMaster.HashService.Domain.Files;

namespace Wally.RomMaster.HashService.Application.Files;

public interface IFileReadOnlyRepository : IReadOnlyRepository<File, FileId>
{
}
