using System;

namespace Wally.RomMaster.FileService.Messages.Files;

public class FileCreatedMessage
{
	public Guid Id { get; }

	public string Location { get; }

	public FileCreatedMessage(Guid id, string location)
	{
		Id = id;
		Location = location;

		new FileCreatedMessageValidator().Validate(this);
	}
}
