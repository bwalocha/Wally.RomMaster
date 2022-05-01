using System;
using System.Diagnostics;
using System.IO;

using Wally.Lib.DDD.Abstractions.DomainModels;
using Wally.RomMaster.Domain.Abstractions;

namespace Wally.RomMaster.Domain.Files;

[DebuggerDisplay("Location = {Location}")]
public class File : AggregateRoot
{
	// Hide public .ctor
#pragma warning disable CS8618
	private File()
#pragma warning restore CS8618
	{
	}

	private File(DateTime timestamp, FileLocation location)
	{
		ModifiedAt = timestamp;
		Location = location;
	}

	public FileLocation Location { get; private set; }

	public long Length { get; private set; }

	public string? Crc { get; private set; }

	public string? Sha1 { get; private set; }

	public string? Md5 { get; private set; }

	public FileAttributes Attributes { get; private set; }

	public DateTime CreationTimeUtc { get; private set; }

	public DateTime LastAccessTimeUtc { get; private set; }

	public DateTime LastWriteTimeUtc { get; private set; }

	public DateTime ModifiedAt { get; private set; }

	public static File Create(IClockService clockService, FileLocation location)
	{
		var model = new File(clockService.GetTimestamp(), location);

		return model;
	}
}
