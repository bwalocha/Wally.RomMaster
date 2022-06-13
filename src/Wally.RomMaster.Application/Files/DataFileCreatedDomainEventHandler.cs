using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Application.Files;

public class DataFileCreatedDomainEventHandler : IDomainEventHandler<DataFileCreatedDomainEvent>
{
	private readonly IFileRepository _fileRepository;

	public DataFileCreatedDomainEventHandler(IFileRepository fileRepository)
	{
		_fileRepository = fileRepository;
	}

	public async Task HandleAsync(DataFileCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var model = await _fileRepository.GetAsync(domainEvent.Id, cancellationToken);

		// TODO: parse
		// ...
	}
}
