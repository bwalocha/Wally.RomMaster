using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Themes;

public class Theme : AggregateRoot<Theme, ThemeId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Theme()
#pragma warning restore CS8618
	{
	}

	private Theme(ThemeId id, string name, string slug, Uri url,  Uri href)
		: base(id)
	{
		Name = name;
		Slug = slug;
		Url = url;
		Href = href;
	}

	private Theme(string name, string slug, Uri url,  Uri href)
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

	public static Theme Create(string name, string slug, Uri url,  Uri href)
	{
		var model = new Theme(name, slug, url, href);
		// model.AddDomainEvent(new KindCreatedDomainEvent(model.Id));

		return model;
	}

	public static Theme Create(ThemeId id, string name, string slug, Uri url,  Uri href)
	{
		var model = new Theme(id, name, slug, url, href);
		// model.AddDomainEvent(new KindCreatedDomainEvent(model.Id));

		return model;
	}
}
