using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Domain.Notifications;

namespace Wally.RomMaster.NotificationService.Application.Notifications.Commands;

[ExcludeFromCodeCoverage]
public sealed class BroadcastNotificationCommand : ICommand<NotificationId>
{
	public NotificationId NotificationId { get; }
	public string Title { get; }
	public string Content { get; }

	public BroadcastNotificationCommand(NotificationId notificationId, string title, string content)
	{
		NotificationId = notificationId;
		Title = title;
		Content = content;
	}

	public BroadcastNotificationCommand(string title, string content)
		: this(new NotificationId(), title, content)
	{
	}
}
