using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Application.Files;

public class MetadataFileCreatedDomainEventHandler : IDomainEventHandler<MetadataFileCreatedDomainEvent>
{
	public MetadataFileCreatedDomainEventHandler(IMetadataFileRepository metadataFileRepository)
	{
	}

	public Task HandleAsync(MetadataFileCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		/*var model = await _fileRepository.GetAsync(domainEvent.Id, cancellationToken);

		if (model.IsZipArchive())
		{
			throw new NotImplementedException("Zip not supported");
		}*/

		return Task.CompletedTask;
	}
}
