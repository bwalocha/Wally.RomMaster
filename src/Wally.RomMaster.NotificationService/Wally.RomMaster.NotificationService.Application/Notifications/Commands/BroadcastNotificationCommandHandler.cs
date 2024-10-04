using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Domain.Notifications;

namespace Wally.RomMaster.NotificationService.Application.Notifications.Commands;

public class BroadcastNotificationCommandHandler : CommandHandler<BroadcastNotificationCommand, NotificationId>
{
	private readonly Hub<IEventHub> _hub;

	public BroadcastNotificationCommandHandler(Hub<IEventHub> hub)
	{
		_hub = hub;
	}

	public override async Task<NotificationId> HandleAsync(BroadcastNotificationCommand command, CancellationToken cancellationToken)
	{
		var model = Notification.Create(command.NotificationId, command.Title, command.Content);

		// await _hub.Clients.All.NewEventAsync(model.Title, model.Content, cancellationToken);
		
		return model.Id;
	}
}
