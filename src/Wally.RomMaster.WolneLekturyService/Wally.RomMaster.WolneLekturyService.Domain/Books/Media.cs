using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Authors;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class Media : Entity<Media, MediaId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Media()
#pragma warning restore CS8618
	{
	}

	private Media(MediaId id, string name, MediaType mediaType, string director, string artist, Uri url)
		: base(id)
	{
		Name = name;
		MediaType = mediaType;
		Director = director;
		Artist = artist;
		Url = url;
	}

	private Media(string name, MediaType mediaType, string director, string artist, Uri url)
	{
		Name = name;
		MediaType = mediaType;
		Director = director;
		Artist = artist;
		Url = url;
	}

	public string Name { get; private set; }
	public MediaType MediaType { get; private set; }
	public string Director { get; private set; }
	public string Artist { get; private set; }
	public Uri Url { get; private set; }

	public bool IsDeleted { get; private set; } = false;
	public DateTimeOffset? DeletedAt { get; private set; } = null;

	public UserId? DeletedById { get; private set; } = null;

	public static Media Create(string name, MediaType mediaType, string director, string artist, Uri url)
	{
		var model = new Media(name, mediaType, director, artist, url);
		// model.AddDomainEvent(new MediaCreatedDomainEvent(model.Id, model.Name));

		return model;
	}

	public static Media Create(MediaId id, string name, MediaType mediaType, string director, string artist, Uri url)
	{
		var model = new Media(id, name, mediaType, director, artist, url);
		// model.AddDomainEvent(new MediaCreatedDomainEvent(model.Id, model.Name));

		return model;
	}
}
