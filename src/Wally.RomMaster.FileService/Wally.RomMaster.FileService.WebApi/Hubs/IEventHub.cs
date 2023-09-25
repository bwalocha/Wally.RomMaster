using System.Threading;
using System.Threading.Tasks;

namespace Wally.RomMaster.FileService.WebApi.Hubs;

public interface IEventHub
{
	public Task NewEventAsync(string user, string @event, CancellationToken cancellationToken);
}
