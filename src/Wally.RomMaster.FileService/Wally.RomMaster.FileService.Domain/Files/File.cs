using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Domain.Files;

[DebuggerDisplay("Location = {Location}")]
public class File : AggregateRoot
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

		// Crc = crc32;
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

		// Crc = crc32;
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

	public Guid PathId { get; private set; }

	public Path Path { get; private set; }

	// public DataFile DataFile { get; private set; }

	// public Guid? DataFileId { get; private set; }

	public static File Create(
		IClockService clockService,
		Path path,
		FileInfo fileInfo
		/*SourceType type,
		HashAlgorithm hashAlgorithm,
		CancellationToken cancellationToken*/)
	{
		/*var crc32 = fileInfo.IsArchivePackage()
			? "-"
			: await ComputeHashAsync(fileInfo, hashAlgorithm, cancellationToken);*/
		var model = new File(clockService.GetTimestamp(), path, fileInfo /*, crc32*/);
		model.AddDomainEvent(new FileCreatedDomainEvent(model.Id));

		/*if (type == SourceType.Output)
		{
		}
		else if (type == SourceType.DatRoot)
		{
			model.AddDomainEvent(new DataFileCreatedDomainEvent(model.Id));
		}
		else if (type == SourceType.Input)
		{
		}*/

		return model;
	}

	public static File Create(IClockService clockService, Path path, File archivePackage, ZipArchiveEntry entry)
	{
		// var crc32 = entry.Crc32.ToString("X2", CultureInfo.InvariantCulture); // TODO: move to another service
		var fullName = new Uri($"{archivePackage.Location.Location.LocalPath}#{entry.FullName}");
		var model = new File(clockService.GetTimestamp(), path, fullName, entry.Length, archivePackage /*, crc32*/);

		return model;
	}

	/*private static async Task<string> ComputeHashAsync(
		FileInfo fileInfo,
		HashAlgorithm hashAlgorithm,
		CancellationToken cancellationToken)
	{
		await using var stream = System.IO.File.Open(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);

		var hash = await hashAlgorithm.ComputeHashAsync(stream, cancellationToken);
		return BitConverter.ToString(hash)
			.Replace("-", null, true, CultureInfo.InvariantCulture);
	}*/

	public void Update(
		IClockService clockService,
		FileInfo fileInfo
		/*HashAlgorithm hashAlgorithm,
		CancellationToken cancellationToken*/)
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

			/*Crc = fileInfo.IsArchivePackage()
				? "-"
				: await ComputeHashAsync(fileInfo, hashAlgorithm, cancellationToken);*/
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

	/*public void SetDataFile(DataFile dataFile)
	{
		DataFile = dataFile;
		DataFileId = dataFile.Id;
	}*/

	public void SetCrc32(string crc32)
	{
		Crc32 = crc32;
	}
}
