using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Epochs;

public class Epoch : AggregateRoot<Epoch, EpochId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Epoch()
#pragma warning restore CS8618
	{
	}

	private Epoch(EpochId id, string name, string slug, Uri url,  Uri href)
		: base(id)
	{
		Name = name;
		Slug = slug;
		Url = url;
		Href = href;
	}

	private Epoch(string name, string slug, Uri url,  Uri href)
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

	public static Epoch Create(string name, string slug, Uri url,  Uri href)
	{
		var model = new Epoch(name, slug, url, href);
		// model.AddDomainEvent(new EpochCreatedDomainEvent(model.Id));

		return model;
	}

	public static Epoch Create(EpochId id, string name, string slug, Uri url,  Uri href)
	{
		var model = new Epoch(id, name, slug, url, href);
		// model.AddDomainEvent(new EpochCreatedDomainEvent(model.Id));

		return model;
	}
}
