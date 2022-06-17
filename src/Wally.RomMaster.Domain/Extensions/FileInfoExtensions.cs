using System;
using System.IO;

namespace Wally.RomMaster.Domain.Extensions;

public static class FileInfoExtensions
{
	public static bool IsArchivePackage(this FileInfo fileInfo)
	{
		return fileInfo.Name.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase) ||
				fileInfo.Name.EndsWith(".gzip", StringComparison.InvariantCultureIgnoreCase) ||
				fileInfo.Name.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase) ||
				fileInfo.Name.EndsWith(".rar", StringComparison.InvariantCultureIgnoreCase) ||
				fileInfo.Name.EndsWith(".7z", StringComparison.InvariantCultureIgnoreCase) || fileInfo.Name.EndsWith(
					".7zip",
					StringComparison.InvariantCultureIgnoreCase);
	}
}
