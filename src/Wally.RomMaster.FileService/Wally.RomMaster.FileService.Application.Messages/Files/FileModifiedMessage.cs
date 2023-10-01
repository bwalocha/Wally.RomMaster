using System;

namespace Wally.RomMaster.FileService.Application.Messages.Files;

public class FileModifiedMessage
{
	public FileModifiedMessage(Guid id, string location)
	{
		Id = id;
		Location = location;

		new FileModifiedMessageValidator().Validate(this);
	}

	public Guid Id { get; }

	public string Location { get; }
}
