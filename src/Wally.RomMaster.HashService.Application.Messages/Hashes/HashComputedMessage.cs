using System;

namespace Wally.RomMaster.HashService.Application.Messages.Hashes;

public class HashComputedMessage
{
	public HashComputedMessage(Guid fileId, string crc32)
	{
		FileId = fileId;
		Crc32 = crc32;

		new HashComputedMessageValidator().Validate(this);
	}

	public Guid FileId { get; }

	public string Crc32 { get; }
}
