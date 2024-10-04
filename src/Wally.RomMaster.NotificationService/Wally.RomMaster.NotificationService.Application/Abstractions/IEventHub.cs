using System.Threading;
using System.Threading.Tasks;

namespace Wally.RomMaster.NotificationService.Application.Abstractions;

public interface IEventHub
{
	public Task NewEventAsync(string user, string @event, CancellationToken cancellationToken);
}
