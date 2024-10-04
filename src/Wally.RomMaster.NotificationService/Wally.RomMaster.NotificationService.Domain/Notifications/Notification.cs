using System;
using Wally.RomMaster.NotificationService.Domain.Abstractions;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Domain.Notifications;

public class Notification : AggregateRoot<Notification, NotificationId>, ISoftDeletable
{
	public string Title { get; }
	public string Content { get; private set; }
	// Hide public .ctor
#pragma warning disable CS8618
	private Notification()
#pragma warning restore CS8618
	{
	}

	private Notification(NotificationId id, string title, string content)
		: base(id)
	{
		Title = title;
		Content = content;
	}

	private Notification(string title, string content)
	{
		Title = title;
		Content = content;
	}

	public bool IsDeleted { get; private set; } = false;
	public DateTimeOffset? DeletedAt { get; private set; } = null;

	public UserId? DeletedById { get; private set; } = null;

	public static Notification Create(string title, string content)
	{
		var model = new Notification(title, content);
		// model.AddDomainEvent(new NotificationCreatedDomainEvent(model.Id));

		return model;
	}

	public static Notification Create(NotificationId id, string title, string content)
	{
		var model = new Notification(id, title, content);
		// model.AddDomainEvent(new NotificationCreatedDomainEvent(model.Id));

		return model;
	}
}
