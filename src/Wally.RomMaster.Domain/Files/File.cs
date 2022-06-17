using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.DomainModels;
using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.Extensions;

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

	private File(DateTime timestamp, FileInfo fileInfo, string crc32)
	{
		ModifiedAt = timestamp;
		Location = FileLocation.Create(new Uri(fileInfo.FullName));
		Length = fileInfo.Length;
		Attributes = fileInfo.Attributes;
		CreationTimeUtc = fileInfo.CreationTimeUtc;
		LastAccessTimeUtc = fileInfo.LastAccessTimeUtc;
		LastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
		Crc = crc32;
	}

	private File(DateTime timestamp, Uri fullName, long length, string crc32, File archivePackage)
	{
		ModifiedAt = timestamp;
		Location = FileLocation.Create(fullName);
		Length = length;
		Attributes = archivePackage.Attributes;
		CreationTimeUtc = archivePackage.CreationTimeUtc;
		LastAccessTimeUtc = archivePackage.LastAccessTimeUtc;
		LastWriteTimeUtc = archivePackage.LastWriteTimeUtc;
		Crc = crc32;
	}

	public FileLocation Location { get; private set; }

	public long Length { get; private set; }

	public string Crc { get; private set; }

	public string? Sha1 { get; private set; }

	public string? Md5 { get; private set; }

	public FileAttributes Attributes { get; private set; }

	public DateTime CreationTimeUtc { get; private set; }

	public DateTime LastAccessTimeUtc { get; private set; }

	public DateTime LastWriteTimeUtc { get; private set; }

	public DateTime ModifiedAt { get; private set; }

	public static async Task<File> CreateAsync(
		IClockService clockService,
		FileInfo fileInfo,
		SourceType type,
		HashAlgorithm hashAlgorithm,
		CancellationToken cancellationToken)
	{
		var crc32 = fileInfo.IsArchivePackage()
			? "-"
			: await ComputeHashAsync(fileInfo, hashAlgorithm, cancellationToken);
		var model = new File(clockService.GetTimestamp(), fileInfo, crc32);

		model.AddDomainEvent(new FileCreatedDomainEvent(model.Id));

		if (type == SourceType.Output)
		{
		}
		else if (type == SourceType.DatRoot)
		{
			model.AddDomainEvent(new DataFileCreatedDomainEvent(model.Id));
		}
		else if (type == SourceType.Input)
		{
		}

		return model;
	}

	public static File Create(IClockService clockService, File archivePackage, ZipArchiveEntry entry)
	{
		var crc32 = entry.Crc32.ToString("X2", CultureInfo.InvariantCulture);
		var fullName = new Uri($"{archivePackage.Location.Location.LocalPath}#{entry.FullName}");
		var model = new File(clockService.GetTimestamp(), fullName, entry.Length, crc32, archivePackage);

		return model;
	}

	private static async Task<string> ComputeHashAsync(
		FileInfo fileInfo,
		HashAlgorithm hashAlgorithm,
		CancellationToken cancellationToken)
	{
		await using var stream = System.IO.File.Open(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);

		var hash = await hashAlgorithm.ComputeHashAsync(stream, cancellationToken);
		return BitConverter.ToString(hash)
			.Replace("-", null, true, CultureInfo.InvariantCulture);
	}

	public async Task UpdateAsync(
		IClockService clockService,
		FileInfo fileInfo,
		HashAlgorithm hashAlgorithm,
		CancellationToken cancellationToken)
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
			Crc = fileInfo.IsArchivePackage()
				? "-"
				: await ComputeHashAsync(fileInfo, hashAlgorithm, cancellationToken);
		}

		ModifiedAt = clockService.GetTimestamp();
	}

	private bool HasChanged(FileInfo fileInfo)
	{
		return Attributes != fileInfo.Attributes || LastWriteTimeUtc != fileInfo.LastWriteTimeUtc;
	}

	public bool IsArchivePackage()
	{
		return Location.Location.LocalPath.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase) ||
				Location.Location.LocalPath.EndsWith(".gzip", StringComparison.InvariantCultureIgnoreCase) ||
				Location.Location.LocalPath.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase) ||
				Location.Location.LocalPath.EndsWith(".rar", StringComparison.InvariantCultureIgnoreCase) ||
				Location.Location.LocalPath.EndsWith(".7z", StringComparison.InvariantCultureIgnoreCase) ||
				Location.Location.LocalPath.EndsWith(".7zip", StringComparison.InvariantCultureIgnoreCase);
	}
}
