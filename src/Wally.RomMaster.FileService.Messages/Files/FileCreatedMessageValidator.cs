namespace Wally.RomMaster.FileService.Messages.Files;

public class FileCreatedMessage
{
	private readonly Guid _id;
	private readonly string _location;

	public FileCreatedMessage(Guid id, string location)
	{
		_id = id;
		_location = location;
		
	}
}
