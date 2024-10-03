using System;
using System.Collections.Generic;
using System.Linq;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Authors;
using Wally.RomMaster.WolneLekturyService.Domain.Epochs;
using Wally.RomMaster.WolneLekturyService.Domain.Genres;
using Wally.RomMaster.WolneLekturyService.Domain.Kinds;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class BookDetail : Entity<BookDetail, BookDetailId>, ISoftDeletable
{
	private readonly List<EpochId> _epochIds;
	private readonly List<Translator> _translators;
	private readonly List<KindId> _kindIds;
	private readonly List<GenreId> _genreIds;
	private readonly List<AuthorId> _authorIds;
	private readonly List<Media> _media;
	// Hide public .ctor
#pragma warning disable CS8618
	private BookDetail()
#pragma warning restore CS8618
	{
	}

	private BookDetail(
		BookDetailId id,
		string title,
		string slug,
		string fullSortKey,
		Uri url,
		Uri href,
		Language language,
		IReadOnlyCollection<KindId> kindIds,
		IReadOnlyCollection<EpochId> epochIds,
		IReadOnlyCollection<GenreId> genreIds,
		IReadOnlyCollection<AuthorId> authorIds,
		IReadOnlyCollection<Translator> translators,
		FragmentData fragmentData,
		// children
		// parent?
		bool preview,
		Uri epub,
		Uri mobi,
		Uri pdf,
		Uri html,
		Uri txt,
		Uri fb2,
		Uri xml,
		IReadOnlyCollection<Media> media,
		TimeSpan audioLength,
		string coverColor,
		Uri simpleCover,
		Uri coverThumb,
		Uri cover,
		Uri simpleThumb,
		Isbn isbnPdf,
		Isbn isbnEpub,
		Isbn isbnMobi)
		: base(id)
	{
		_epochIds = epochIds.ToList();
		_translators = translators.ToList();
		Title = title;
		Slug = slug;
		FullSortKey = fullSortKey;
		Url = url;
		Href = href;
		Language = language;
		_kindIds = kindIds.ToList();
		_genreIds = genreIds.ToList();
		_authorIds = authorIds.ToList();
		FragmentData = fragmentData;
		Preview = preview;
		Epub = epub;
		Mobi = mobi;
		Pdf = pdf;
		Html = html;
		Txt = txt;
		Fb2 = fb2;
		Xml = xml;
		_media = media.ToList();
		AudioLength = audioLength;
		Cover = cover;
		CoverThumb = coverThumb;
		CoverColor = coverColor;
		SimpleCover = simpleCover;
		SimpleThumb = simpleThumb;
		IsbnPdf = isbnPdf;
		IsbnEpub = isbnEpub;
		IsbnMobi = isbnMobi;
	}

	private BookDetail(string title,
		string slug,
		string fullSortKey,
		Uri url,
		Uri href,
		Language language,
		IReadOnlyCollection<KindId> kindIds,
		IReadOnlyCollection<EpochId> epochIds,
		IReadOnlyCollection<GenreId> genreIds,
		IReadOnlyCollection<AuthorId> authorIds,
		IReadOnlyCollection<Translator> translators,
		FragmentData fragmentData,
		// children
		// parent?
		bool preview,
		Uri epub,
		Uri mobi,
		Uri pdf,
		Uri html,
		Uri txt,
		Uri fb2,
		Uri xml,
		IReadOnlyCollection<Media> media,
		TimeSpan audioLength,
		string coverColor,
		Uri simpleCover,
		Uri coverThumb,
		Uri cover,
		Uri simpleThumb,
		Isbn isbnPdf,
		Isbn isbnEpub,
		Isbn isbnMobi)
	{
		_epochIds = epochIds.ToList();
		_translators = translators.ToList();
		Title = title;
		Slug = slug;
		FullSortKey = fullSortKey;
		Url = url;
		Href = href;
		Language = language;
		_kindIds = kindIds.ToList();
		_genreIds = genreIds.ToList();
		_authorIds = authorIds.ToList();
		FragmentData = fragmentData;
		Preview = preview;
		Epub = epub;
		Mobi = mobi;
		Pdf = pdf;
		Html = html;
		Txt = txt;
		Fb2 = fb2;
		Xml = xml;
		_media = media.ToList();
		AudioLength = audioLength;
		Cover = cover;
		CoverThumb = coverThumb;
		CoverColor = coverColor;
		SimpleCover = simpleCover;
		SimpleThumb = simpleThumb;
		IsbnPdf = isbnPdf;
		IsbnEpub = isbnEpub;
		IsbnMobi = isbnMobi;
	}

	public string Title { get; private set; }
	public string Slug { get; private set; }
	public string FullSortKey { get; private set; }
	public Uri Url { get; private set; }
	public Uri Href { get; private set; }
	public Language Language { get; private set; }
	public IReadOnlyCollection<EpochId> EpochIds => _epochIds.AsReadOnly();
	public IReadOnlyCollection<Translator> Translators => _translators.AsReadOnly();
	public IReadOnlyCollection<KindId> KindIds => _kindIds.AsReadOnly();
	public IReadOnlyCollection<GenreId> GenreIds => _genreIds.AsReadOnly();
	public IReadOnlyCollection<AuthorId> AuthorIds => _authorIds.AsReadOnly();
	public FragmentData FragmentData { get; }
	public bool Preview { get; }
	public Uri Epub { get; private set; }
	public Uri Mobi { get; private set; }
	public Uri Pdf { get; private set; }
	public Uri Html { get; private set; }
	public Uri Txt { get; private set; }
	public Uri Fb2 { get; private set; }
	public Uri Xml { get; private set; }
	public IReadOnlyCollection<Media> Media => _media.AsReadOnly();
	public TimeSpan AudioLength { get; private set; }
	public Uri Cover { get; }
	public Uri CoverThumb { get; private set; }
	public string CoverColor { get; private set; }
	public Uri SimpleCover { get; private set; }
	public Uri SimpleThumb { get; private set; }
	public Isbn IsbnPdf { get; private set; }
	public Isbn IsbnEpub { get; private set; }
	public Isbn IsbnMobi { get; private set; }

	public bool IsDeleted { get; private set; } = false;
	public DateTimeOffset? DeletedAt { get; private set; } = null;

	public UserId? DeletedById { get; private set; } = null;

	public static BookDetail Create(string title,
		string slug,
		string fullSortKey,
		Uri url,
		Uri href,
		Language language,
		IReadOnlyCollection<KindId> kindIds,
		IReadOnlyCollection<EpochId> epochIds,
		IReadOnlyCollection<GenreId> genreIds,
		IReadOnlyCollection<AuthorId> authorIds,
		IReadOnlyCollection<Translator> translators,
		FragmentData fragmentData,
		// children
		// parent?
		bool preview,
		Uri epub,
		Uri mobi,
		Uri pdf,
		Uri html,
		Uri txt,
		Uri fb2,
		Uri xml,
		IReadOnlyCollection<Media> media,
		TimeSpan audioLength,
		string coverColor,
		Uri simpleCover,
		Uri coverThumb,
		Uri cover,
		Uri simpleThumb,
		Isbn isbnPdf,
		Isbn isbnEpub,
		Isbn isbnMobi)
	{
		var model = new BookDetail(title, slug, fullSortKey, url, href, language, kindIds, epochIds, genreIds,
			authorIds, translators, fragmentData, preview, epub, mobi, pdf, html, txt, fb2, xml, media, audioLength,
			coverColor, simpleCover, coverThumb, cover, simpleThumb, isbnPdf, isbnEpub, isbnMobi);
		// model.AddDomainEvent(new BookDetailCreatedDomainEvent(model.Id));

		return model;
	}

	public static BookDetail Create(BookDetailId id,
		string title,
		string slug,
		string fullSortKey,
		Uri url,
		Uri href,
		Language language,
		IReadOnlyCollection<KindId> kindIds,
		IReadOnlyCollection<EpochId> epochIds,
		IReadOnlyCollection<GenreId> genreIds,
		IReadOnlyCollection<AuthorId> authorIds,
		IReadOnlyCollection<Translator> translators,
		FragmentData fragmentData,
		// children
		// parent?
		bool preview,
		Uri epub,
		Uri mobi,
		Uri pdf,
		Uri html,
		Uri txt,
		Uri fb2,
		Uri xml,
		IReadOnlyCollection<Media> media,
		TimeSpan audioLength,
		string coverColor,
		Uri simpleCover,
		Uri coverThumb,
		Uri cover,
		Uri simpleThumb,
		Isbn isbnPdf,
		Isbn isbnEpub,
		Isbn isbnMobi)
	{
		var model = new BookDetail(id, title, slug, fullSortKey, url, href, language, kindIds, epochIds, genreIds,
			authorIds, translators, fragmentData, preview, epub, mobi, pdf, html, txt, fb2, xml, media, audioLength,
			coverColor, simpleCover, coverThumb, cover, simpleThumb, isbnPdf, isbnEpub, isbnMobi);
		// model.AddDomainEvent(new BookDetailCreatedDomainEvent(model.Id));

		return model;
	}
}
