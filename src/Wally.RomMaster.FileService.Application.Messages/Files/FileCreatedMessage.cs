using System;

namespace Wally.RomMaster.FileService.Application.Messages.Files;

public class FileCreatedMessage
{
	public FileCreatedMessage(Guid id, string location)
	{
		Id = id;
		Location = location;

		new FileCreatedMessageValidator().Validate(this);
	}

	public Guid Id { get; }

	public string Location { get; }
}
