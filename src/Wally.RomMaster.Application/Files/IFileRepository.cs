using System;

using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Application.Files;

public interface IFileRepository : IRepository<File>
{
	void RemoveOutdatedFiles(DateTime timestamp);
}
