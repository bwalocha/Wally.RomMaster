using System.IO;
using File = Wally.RomMaster.FileService.Domain.Files.File;

namespace Wally.RomMaster.FileService.Domain.Extensions;

public static class FileInfoExtensions
{
	public static bool IsArchivePackage(this FileInfo fileInfo)
	{
		return File.IsArchivePackage(fileInfo.Name);
	}
}
