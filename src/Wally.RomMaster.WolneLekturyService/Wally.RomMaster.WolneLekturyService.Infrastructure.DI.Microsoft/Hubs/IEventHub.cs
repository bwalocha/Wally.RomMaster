using System.Threading;
using System.Threading.Tasks;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Hubs;

public interface IEventHub
{
	public Task NewEventAsync(string user, string @event, CancellationToken cancellationToken);
}
