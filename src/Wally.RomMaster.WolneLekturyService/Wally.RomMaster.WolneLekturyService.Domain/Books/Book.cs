using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Authors;
using Wally.RomMaster.WolneLekturyService.Domain.Epochs;
using Wally.RomMaster.WolneLekturyService.Domain.Genres;
using Wally.RomMaster.WolneLekturyService.Domain.Kinds;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class Book : AggregateRoot<Book, BookId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Book()
#pragma warning restore CS8618
	{
	}

	private Book(
		BookId id,
		string title,
		string slug,
		string fullSortKey,
		Uri url,
		Uri href,
		KindId kindId,
		EpochId epochId,
		GenreId genreId,
		AuthorId authorId,
		Uri cover,
		Uri coverThumb,
		string coverColor,
		Uri simpleThumb,
		bool hasAudio,
		bool liked)
		: base(id)
	{
		Title = title;
		Slug = slug;
		FullSortKey = fullSortKey;
		Url = url;
		Href = href;
		KindId = kindId;
		EpochId = epochId;
		GenreId = genreId;
		AuthorId = authorId;
		Cover = cover;
		CoverThumb = coverThumb;
		CoverColor = coverColor;
		SimpleThumb = simpleThumb;
		HasAudio = hasAudio;
		Liked = liked;
	}

	private Book(string title, string slug, string fullSortKey, Uri url, Uri href, KindId kindId,
		EpochId epochId,
		GenreId genreId, AuthorId authorId, Uri cover, Uri coverThumb, string coverColor, Uri simpleThumb,
		bool hasAudio, bool liked)
	{
		Title = title;
		Slug = slug;
		FullSortKey = fullSortKey;
		Url = url;
		Href = href;
		KindId = kindId;
		EpochId = epochId;
		GenreId = genreId;
		AuthorId = authorId;
		Cover = cover;
		CoverThumb = coverThumb;
		CoverColor = coverColor;
		SimpleThumb = simpleThumb;
		HasAudio = hasAudio;
		Liked = liked;
	}

	public string Title { get; private set; }
	public string Slug { get; private set; }
	public string FullSortKey { get; private set; }
	public Uri Url { get; private set; }
	public Uri Href { get; private set; }
	public KindId KindId { get; private set; }
	public EpochId EpochId { get; private set; }
	public GenreId GenreId { get; private set; }
	public AuthorId AuthorId { get; }
	public Uri Cover { get; }
	public Uri CoverThumb { get; private set; }
	public string CoverColor { get; private set; }
	public Uri SimpleThumb { get; private set; }
	public bool HasAudio { get; private set; }
	public bool Liked { get; private set; }

	public bool IsDeleted { get; private set; } = false;
	public DateTimeOffset? DeletedAt { get; private set; } = null;

	public UserId? DeletedById { get; private set; } = null;

	public static Book Create(string title, string slug, string fullSortKey, Uri url, Uri href, KindId kindId,
		EpochId epochId,
		GenreId genreId, AuthorId authorId, Uri cover, Uri coverThumb, string coverColor, Uri simpleThumb,
		bool hasAudio, bool liked)
	{
		var model = new Book(title, slug, fullSortKey, url, href, kindId, epochId, genreId, authorId, cover, coverThumb,
			coverColor, simpleThumb, hasAudio, liked);
		// model.AddDomainEvent(new BookCreatedDomainEvent(model.Id));

		return model;
	}

	public static Book Create(BookId id, string title, string slug, string fullSortKey, Uri url, Uri href,
		KindId kindId,
		EpochId epochId,
		GenreId genreId, AuthorId authorId, Uri cover, Uri coverThumb, string coverColor, Uri simpleThumb,
		bool hasAudio, bool liked)
	{
		var model = new Book(id, title, slug, fullSortKey, url, href, kindId, epochId, genreId, authorId, cover,
			coverThumb, coverColor, simpleThumb, hasAudio, liked);
		// model.AddDomainEvent(new BookCreatedDomainEvent(model.Id));

		return model;
	}
}
