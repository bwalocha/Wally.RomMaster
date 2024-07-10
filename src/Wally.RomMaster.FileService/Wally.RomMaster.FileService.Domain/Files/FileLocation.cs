using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Domain.Files;

public class FileLocation : ValueObject<FileLocation, Uri>
{
	public FileLocation(Uri value)
		: base(value)
	{
	}

	// https://stackoverflow.com/questions/17408499/async-wait-for-file-to-be-created
	public bool IsFileLocked()
	{
		try
		{
			using (System.IO.File.Open(Value.LocalPath, FileMode.Open))
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
	
	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}
