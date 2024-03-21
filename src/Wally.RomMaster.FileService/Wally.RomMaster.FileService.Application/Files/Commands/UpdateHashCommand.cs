using System.Diagnostics.CodeAnalysis;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

[ExcludeFromCodeCoverage]
public sealed class UpdateHashCommand : ICommand
{
	public UpdateHashCommand(FileId fileId, string crc32, string md5)
	{
		FileId = fileId;
		Crc32 = crc32;
		Md5 = md5;
	}

	public FileId FileId { get; }

	public string Crc32 { get; }
	public string Md5 { get; }
}
