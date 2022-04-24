using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

using Wally.Lib.DDD.Abstractions.DomainModels;

namespace Wally.RomMaster.Domain.Files;

[DebuggerDisplay("Location = {Location}")]
public class FileLocation : ValueObject
{
	private FileLocation(Uri location)
	{
		Location = location;
	}

	public Uri Location { get; }

	public static FileLocation Create(Uri location)
	{
		return new FileLocation(location);
	}

	// https://stackoverflow.com/questions/17408499/async-wait-for-file-to-be-created
	public bool IsFileLocked()
	{
		try
		{
			using (System.IO.File.Open(Location.LocalPath, FileMode.Open))
			{
			}
		}
		catch (IOException e)
		{
			var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);
			return errorCode is 32 or 33;
		}

		return false;
	}
}
