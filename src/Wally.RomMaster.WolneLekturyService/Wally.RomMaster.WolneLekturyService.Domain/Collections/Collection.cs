using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Collections;

public class Collection : AggregateRoot<Collection, CollectionId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Collection()
#pragma warning restore CS8618
	{
	}

	private Collection(CollectionId id, string title, Uri url,  Uri href)
		: base(id)
	{
		Title = title;
		Url = url;
		Href = href;
	}

	private Collection(string title, Uri url,  Uri href)
	{
		Title = title;
		Url = url;
		Href = href;
	}

	public string Title { get; private set; }
	public Uri Url { get; private set; }
	public Uri Href { get; }

	public bool IsDeleted { get; private set; } = false;
	public DateTimeOffset? DeletedAt { get; private set; } = null;

	public UserId? DeletedById { get; private set; } = null;

	public static Collection Create(string title, Uri url,  Uri href)
	{
		var model = new Collection(title, url, href);
		// model.AddDomainEvent(new CollectionCreatedDomainEvent(model.Id));

		return model;
	}

	public static Collection Create(CollectionId id, string title, Uri url,  Uri href)
	{
		var model = new Collection(id, title, url, href);
		// model.AddDomainEvent(new CollectionCreatedDomainEvent(model.Id));

		return model;
	}
}
