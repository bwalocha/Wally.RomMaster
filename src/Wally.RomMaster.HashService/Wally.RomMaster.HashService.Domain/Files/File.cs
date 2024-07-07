using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using Wally.RomMaster.HashService.Domain.Abstractions;

namespace Wally.RomMaster.HashService.Domain.Files;

[DebuggerDisplay("Location = {Location}")]
public class File : AggregateRoot<File, FileId>
{
	// Hide public .ctor
#pragma warning disable CS8618
	private File()
#pragma warning restore CS8618
	{
	}

	private File(FileId id, FileInfo fileInfo)
		: base(id)
	{
		// ModifiedAt = timestamp;
		Location = new FileLocation(new Uri(fileInfo.FullName));
		Length = fileInfo.Length;
		Attributes = fileInfo.Attributes;
		CreationTimeUtc = fileInfo.CreationTimeUtc;
		LastAccessTimeUtc = fileInfo.LastAccessTimeUtc;
		LastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
	}

	private File(FileId id, FileLocation fileLocation, long length, File archivePackage)
		: base(id)
	{
		// ModifiedAt = timestamp;
		Location = fileLocation;
		Length = length;
		Attributes = archivePackage.Attributes;
		CreationTimeUtc = archivePackage.CreationTimeUtc;
		LastAccessTimeUtc = archivePackage.LastAccessTimeUtc;
		LastWriteTimeUtc = archivePackage.LastWriteTimeUtc;
	}

	public FileLocation Location { get; private set; }

	public long Length { get; private set; }

	public string? Crc32 { get; private set; }

	// public string? Sha1 { get; private set; }
	//
	public string? Md5 { get; private set; }

	public FileAttributes Attributes { get; private set; }

	public DateTime CreationTimeUtc { get; private set; }

	public DateTime LastAccessTimeUtc { get; private set; }

	public DateTime LastWriteTimeUtc { get; private set; }

	// public DateTime ModifiedAt { get; private set; }

	public static File Create(
		FileId id,
		// IClockService clockService,
		FileInfo fileInfo)
	{
		var model = new File(id, fileInfo);

		return model;
	}

	// public static File Create(File archivePackage, ZipArchiveEntry entry)
	// {
	// 	var fullName = new Uri($"{archivePackage.Location.Value.LocalPath}#{entry.FullName}");
	// 	var model = new File(new FileLocation(fullName), entry.Length, archivePackage /*, crc32*/);
	// 	// model.AddDomainEvent(new FileCreatedDomainEvent(model.Id));
	//
	// 	return model;
	// }

	public File Update(
		// IClockService clockService,
		FileInfo fileInfo)
	{
		if (HasChanged(fileInfo))
		{
			// TODO: If the file is a Zip Archive then recreate also inner file entries
			// ...

			Length = fileInfo.Length;
			Attributes = fileInfo.Attributes;
			CreationTimeUtc = fileInfo.CreationTimeUtc;
			LastAccessTimeUtc = fileInfo.LastAccessTimeUtc;
			LastWriteTimeUtc = fileInfo.LastWriteTimeUtc;

			// AddDomainEvent(new FileModifiedDomainEvent(Id));
		}

		// ModifiedAt = clockService.GetTimestamp();

		return this;
	}

	public bool HasChanged(FileInfo fileInfo)
	{
		return LastWriteTimeUtc != fileInfo.LastWriteTimeUtc;
		// return Attributes != fileInfo.Attributes || LastWriteTimeUtc != fileInfo.LastWriteTimeUtc;
	}

	public bool IsArchivePackage()
	{
		return IsArchivePackage(Location.Value.LocalPath);
	}

	internal static bool IsArchivePackage(string fileName)
	{
		return fileName.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase) ||
			fileName.EndsWith(".gzip", StringComparison.InvariantCultureIgnoreCase) ||
			fileName.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase) ||
			fileName.EndsWith(".rar", StringComparison.InvariantCultureIgnoreCase) ||
			fileName.EndsWith(".7z", StringComparison.InvariantCultureIgnoreCase) || fileName.EndsWith(
				".7zip",
				StringComparison.InvariantCultureIgnoreCase);
	}

	public File SetCrc32(string value)
	{
		Crc32 = value;

		return this;
	}

	public File SetMd5(string value)
	{
		Md5 = value;

		return this;
	}
}
