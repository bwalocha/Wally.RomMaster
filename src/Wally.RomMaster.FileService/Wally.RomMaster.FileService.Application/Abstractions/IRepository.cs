using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Application.Abstractions;

public interface IRepository<TAggregateRoot, TStronglyTypedId> : IReadOnlyRepository<TAggregateRoot, TStronglyTypedId>
	where TAggregateRoot : AggregateRoot<TAggregateRoot, TStronglyTypedId>
	where TStronglyTypedId : new()
{
	Task<TAggregateRoot> GetAsync(TStronglyTypedId id, CancellationToken cancellationToken);

	TAggregateRoot Add(TAggregateRoot aggregateRoot);

	TAggregateRoot Update(TAggregateRoot aggregateRoot);

	TAggregateRoot Remove(TAggregateRoot aggregateRoot);
}
