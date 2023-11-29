using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Domain.Files;

[DebuggerDisplay("Location = {Location}")]
public class File : AggregateRoot<File, FileId>
{
	// Hide public .ctor
#pragma warning disable CS8618
	private File()
#pragma warning restore CS8618
	{
	}

	private File(DateTime timestamp, Path path, FileInfo fileInfo /*, string crc32*/)
	{
		// ModifiedAt = timestamp;
		PathId = path.Id;
		Path = path;
		Location = FileLocation.Create(new Uri(fileInfo.FullName));
		Length = fileInfo.Length;
		Attributes = fileInfo.Attributes;
		CreationTimeUtc = fileInfo.CreationTimeUtc;
		LastAccessTimeUtc = fileInfo.LastAccessTimeUtc;
		LastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
	}

	private File(DateTime timestamp, Path path, Uri fullName, long length, File archivePackage /*, string crc32*/)
	{
		// ModifiedAt = timestamp;
		PathId = path.Id;
		Path = path;
		Location = FileLocation.Create(fullName);
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
	// public string? Md5 { get; private set; }

	public FileAttributes Attributes { get; private set; }

	public DateTime CreationTimeUtc { get; private set; }

	public DateTime LastAccessTimeUtc { get; private set; }

	public DateTime LastWriteTimeUtc { get; private set; }

	// public DateTime ModifiedAt { get; private set; }

	public PathId PathId { get; private set; }

	public Path Path { get; private set; }

	public static File Create(
		IClockService clockService,
		Path path,
		FileInfo fileInfo)
	{
		var model = new File(clockService.GetTimestamp(), path, fileInfo);
		model.AddDomainEvent(new FileCreatedDomainEvent(model.Id));

		return model;
	}

	public static File Create(IClockService clockService, Path path, File archivePackage, ZipArchiveEntry entry)
	{
		var fullName = new Uri($"{archivePackage.Location.Location.LocalPath}#{entry.FullName}");
		var model = new File(clockService.GetTimestamp(), path, fullName, entry.Length, archivePackage /*, crc32*/);
		// model.AddDomainEvent(new FileCreatedDomainEvent(model.Id));

		return model;
	}

	public void Update(
		IClockService clockService,
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

			AddDomainEvent(new FileModifiedDomainEvent(Id));
		}

		// ModifiedAt = clockService.GetTimestamp();
	}

	private bool HasChanged(FileInfo fileInfo)
	{
		return Attributes != fileInfo.Attributes || LastWriteTimeUtc != fileInfo.LastWriteTimeUtc;
	}

	public bool IsArchivePackage()
	{
		return IsArchivePackage(Location.Location.LocalPath);
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

	public void SetCrc32(string crc32)
	{
		Crc32 = crc32;
	}
}
