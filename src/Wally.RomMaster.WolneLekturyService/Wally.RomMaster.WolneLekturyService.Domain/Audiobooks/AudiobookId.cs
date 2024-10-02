using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Audiobooks;

public class AudiobookId : GuidId<AudiobookId>
{
	public AudiobookId()
	{
	}

	public AudiobookId(Guid value)
		: base(value)
	{
	}
}
