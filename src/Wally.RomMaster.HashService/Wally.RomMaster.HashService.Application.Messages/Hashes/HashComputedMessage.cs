using System;

namespace Wally.RomMaster.HashService.Application.Messages.Hashes;

public class HashComputedMessage
{
	public HashComputedMessage(Guid fileId, string crc32, string md5)
	{
		FileId = fileId;
		Crc32 = crc32;
		Md5 = md5;
		
		new HashComputedMessageValidator().Validate(this);
	}
	
	public Guid FileId { get; }
	
	public string Crc32 { get; }
	
	public string Md5 { get; }
}
