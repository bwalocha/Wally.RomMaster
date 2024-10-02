using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Authors;

public class Author : AggregateRoot<Author, AuthorId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Author()
#pragma warning restore CS8618
	{
	}

	private Author(AuthorId id, string name, string slug, Uri url,  Uri href)
		: base(id)
	{
		Name = name;
		Slug = slug;
		Url = url;
		Href = href;
	}

	private Author(string name, string slug, Uri url,  Uri href)
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

	public static Author Create(string name, string slug, Uri url,  Uri href)
	{
		var model = new Author(name, slug, url, href);
		// model.AddDomainEvent(new AuthorCreatedDomainEvent(model.Id));

		return model;
	}

	public static Author Create(AuthorId id, string name, string slug, Uri url,  Uri href)
	{
		var model = new Author(id, name, slug, url, href);
		// model.AddDomainEvent(new AuthorCreatedDomainEvent(model.Id));

		return model;
	}
}
