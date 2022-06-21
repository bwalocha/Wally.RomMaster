using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Application.Files;

public class DataFileCreatedDomainEventHandler : IDomainEventHandler<DataFileCreatedDomainEvent>
{
	private readonly IDataFileParser _dataFileParser;
	private readonly IFileRepository _fileRepository;

	public DataFileCreatedDomainEventHandler(IFileRepository fileRepository, IDataFileParser dataFileParser)
	{
		_fileRepository = fileRepository;
		_dataFileParser = dataFileParser;
	}

	public async Task HandleAsync(DataFileCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var file = await _fileRepository.GetAsync(domainEvent.Id, cancellationToken);

		var model = await _dataFileParser.ParseAsync(file.Location, cancellationToken);
		file.SetDataFile(model);

		_fileRepository.Update(file);
	}
}
