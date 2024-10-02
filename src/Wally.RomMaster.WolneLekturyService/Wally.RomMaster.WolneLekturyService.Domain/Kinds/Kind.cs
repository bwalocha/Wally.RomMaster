using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Kinds;

public class Kind : AggregateRoot<Kind, KindId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Kind()
#pragma warning restore CS8618
	{
	}

	private Kind(KindId id, string name, string slug, Uri url,  Uri href)
		: base(id)
	{
		Name = name;
		Slug = slug;
		Url = url;
		Href = href;
	}

	private Kind(string name, string slug, Uri url,  Uri href)
	{
		Name = name;
		Slug = slug;
		Url = url;
		Href = href;
	}

	public string Name { get; private set; }
	public string Slug { get; private set; }
	public Uri Url { get; private set; }
	public Uri Href { get; }

	public bool IsDeleted { get; private set; } = false;
	public DateTimeOffset? DeletedAt { get; private set; } = null;

	public UserId? DeletedById { get; private set; } = null;

	public static Kind Create(string name, string slug, Uri url,  Uri href)
	{
		var model = new Kind(name, slug, url, href);
		// model.AddDomainEvent(new KindCreatedDomainEvent(model.Id));

		return model;
	}

	public static Kind Create(KindId id, string name, string slug, Uri url,  Uri href)
	{
		var model = new Kind(id, name, slug, url, href);
		// model.AddDomainEvent(new KindCreatedDomainEvent(model.Id));

		return model;
	}
}
