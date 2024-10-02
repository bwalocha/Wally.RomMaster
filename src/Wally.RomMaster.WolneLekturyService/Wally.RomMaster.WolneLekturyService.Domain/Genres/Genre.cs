using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Genres;

public class Genre : AggregateRoot<Genre, GenreId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Genre()
#pragma warning restore CS8618
	{
	}

	private Genre(GenreId id, string name, string slug, Uri url,  Uri href)
		: base(id)
	{
		Name = name;
		Slug = slug;
		Url = url;
		Href = href;
	}

	private Genre(string name, string slug, Uri url,  Uri href)
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

	public static Genre Create(string name, string slug, Uri url,  Uri href)
	{
		var model = new Genre(name, slug, url, href);
		// model.AddDomainEvent(new GenreCreatedDomainEvent(model.Id));

		return model;
	}

	public static Genre Create(GenreId id, string name, string slug, Uri url,  Uri href)
	{
		var model = new Genre(id, name, slug, url, href);
		// model.AddDomainEvent(new GenreCreatedDomainEvent(model.Id));

		return model;
	}
}
