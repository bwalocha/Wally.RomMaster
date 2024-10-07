using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Books;
using Xunit;

namespace Wally.RomMaster.WolneLekturyService.Tests.IntegrationTests.Infrastructure.WolneLekturyApiClient;

public class ApiClientBooksTests
{
	private const string WatchFolder = "d:/WatchFolder";
	private const string BooksFolder = @"\\192.168.1.2\Audio\-\audiobook\WolneLektury";

	private const string MetadataTemplate = """
											<?xml version="1.0" encoding="utf-8"?>
											<ns0:package xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:ns0="http://www.idpf.org/2007/opf" unique-identifier="BookId" version="2.0">
											  <ns0:metadata xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:opf="http://www.idpf.org/2007/opf">
											    <dc:title>{TITLE}</dc:title>
											    <dc:subtitle>{SUBTITLE}</dc:subtitle>
											    
											    {AUTHORS}
												<dc:date>{PUBLISH_YEAR}</dc:date>
											    
												<dc:description>{DESCRIPTION}</dc:description>
												
												{GENRES}
												{TAGS}
											
												{NARRATORS}
												
											    <dc:identifier opf:scheme="ISBN">{ISBN}</dc:identifier>
											    <dc:identifier opf:scheme="ASIN">{ASIN}</dc:identifier>
											
												<dc:publisher>{PUBLISHER}</dc:publisher>
												<dc:language>{LANGUAGE}</dc:language>
												<!-- {EXPLICIT} -->
												<!-- {ABRIDGED} -->
											  </ns0:metadata>
											</ns0:package>

											""";

	private readonly ApiClient _apiClient;

	public ApiClientBooksTests()
	{
		_apiClient = new ApiClient(new HttpClient());
	}

	[Fact(Skip = "Disabled")]
	public async Task GetBooks_ForValidArguments_ReturnsResponse()
	{
		// Arrange

		// Act
		var response = await _apiClient.GetBooksAsync();

		// Assert
		response.Should()
			.NotBeNull();
		response!.Length.Should()
			.Be(7018);
		foreach (var resource in response!)
		{
			resource.Should()
				.NotBeNull();

			var details = await _apiClient.GetBookAsync(resource.Slug);

			details.Should()
				.NotBeNull();

			if (File.Exists(Path.Combine(WatchFolder, $"{resource.Slug}.crawljob")))
			{
				continue;
			}

			await Task.Delay(TimeSpan.FromSeconds(1));

			await using var outputFile = new StreamWriter(Path.Combine(WatchFolder, $"{resource.Slug}.crawljob"));

			async Task write(string slug, Uri url, bool last = false)
			{
				if (!url.IsAbsoluteUri)
				{
					return;
				}

				await outputFile.WriteLineAsync("  {");
				await outputFile.WriteLineAsync($"""
												"packageName": "WolneLekturyPl-{slug}",
												"comment": "{slug}",
												"enabled": "TRUE",
												"autoStart": "TRUE",
												"forcedStart": "FALSE",
												"autoConfirm": "TRUE",
												"text": "{url}",
												"filename": "{Path.GetFileName(url.AbsolutePath)}"
												""");
				if (last)
				{
					await outputFile.WriteLineAsync("  }");
				}
				else
				{
					await outputFile.WriteLineAsync("  },");
				}
			}

			await outputFile.WriteLineAsync("[");

			await write(resource.Slug, details!.Epub);
			await write(resource.Slug, details.Mobi);
			await write(resource.Slug, details.Pdf);
			await write(resource.Slug, details.Html);
			await write(resource.Slug, details.Txt);
			await write(resource.Slug, details.Fb2);
			await write(resource.Slug, details.Xml);

			foreach (var media in details.Media)
			{
				await write(resource.Slug, media.Url);
			}

			await write(resource.Slug, details.SimpleCover);
			await write(resource.Slug, details.CoverThumb);
			await write(resource.Slug, details.Cover);
			await write(resource.Slug, details.SimpleThumb, true);

			await outputFile.WriteLineAsync($"]");
		}
	}

	[Fact(Skip = "Disabled")]
	public async Task GetBooks_ForExistingFiles_CreatesMetadataFile()
	{
		// Arrange

		// Act
		var response = await _apiClient.GetBooksAsync();

		// Assert
		response.Should()
			.NotBeNull();
		response!.Length.Should()
			.Be(7018);
		foreach (var resource in response!)
		{
			resource.Should()
				.NotBeNull();

			var details = await _apiClient.GetBookAsync(resource.Slug);

			details.Should()
				.NotBeNull();

			if (File.Exists(Path.Combine(BooksFolder, resource.Slug, "metadata.opf")))
			{
				continue;
			}

			await Task.Delay(TimeSpan.FromSeconds(1));

			string StripHtml(string html)
			{
				var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
				return reg.Replace(html, "");
			}

			var content = MetadataTemplate
				.Replace("{TITLE}", details!.Title)
				.Replace("{SUBTITLE}", string.Join(", ", details.Translators.Select(a => a.Name)))
				.Replace("{ISBN}", details.IsbnPdf)
				.Replace("{ASIN}", string.Empty)
				.Replace("{PUBLISHER}", "Fundacja Wolne Lektury")
				.Replace("{PUBLISH_YEAR}", string.Empty)
				.Replace("{LANGUAGE}", details.Language)
				.Replace("{NARRATORS}", string.Empty)
				.Replace("{AUTHORS}",
					string.Join("\r\n",
						details.Authors.Select(a => $"<dc:creator opf:role=\"aut\">{a.Name}</dc:creator>")))
				.Replace("{DESCRIPTION}", StripHtml(details.FragmentData.Html))
				.Replace("{GENRES}",
					string.Join("\r\n", details.Genres.Select(a => $"<dc:subject>{a.Name}</dc:subject>")))
				.Replace("{TAGS}", string.Join("\r\n", details.Epochs.Select(a => $"<dc:tag>{a.Name}</dc:tag>")
					.Concat(details.Kinds.Select(a => $"<dc:tag>{a.Name}</dc:tag>"))));

			await using var outputFile = new StreamWriter(Path.Combine(BooksFolder, resource.Slug, "metadata.opf"));
			await outputFile.WriteLineAsync(content);
		}
	}

	[Fact(Skip = "Disabled")]
	public async Task GetBooks_ForAllMedia_FileExists()
	{
		// Arrange

		// Act
		var response = await _apiClient.GetBooksAsync();

		// Assert
		response.Should()
			.NotBeNull();
		response!.Length.Should()
			.Be(7018);
		foreach (var resource in response!)
		{
			resource.Should()
				.NotBeNull();

			var details = await _apiClient.GetBookAsync(resource.Slug);

			details.Should()
				.NotBeNull();

			var urls = new[]
			{
				details!.CoverThumb,
				// details.Html,
				details.SimpleThumb,
				details.Epub,
				// details.Fb2,
				details.Mobi,
				details.Pdf,
				details.Txt,
				// details.Xml,
				details.SimpleCover,
			};

			var files = urls.Where(a => a.IsAbsoluteUri)
				.Select(a => Path.GetFileName(a.AbsolutePath));

			foreach (var file in files)
			{
				File.Exists(Path.Combine(BooksFolder, resource.Slug, file))
					.Should()
					.BeTrue("File Not Found: {0}", Path.Combine(BooksFolder, resource.Slug, file));
			}
		}
	}
}
