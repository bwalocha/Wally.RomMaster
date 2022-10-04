using System.Text.RegularExpressions;

using Wally.RomMaster.ArchiveOrgDownloader.Models;
using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.DataFiles;
using Wally.RomMaster.Domain.Files;

using File = System.IO.File;
using Path = System.IO.Path;

namespace Wally.RomMaster.ArchiveOrgDownloader;

internal class Downloader
{
	private readonly AppSettings _appSettings;
	private readonly HttpClient _httpClient;
	private readonly IDataFileParser _parser;

	public Downloader(IDataFileParser parser, HttpClient httpClient, AppSettings appSettings)
	{
		_parser = parser;
		_httpClient = httpClient;
		_appSettings = appSettings;
	}

	public async Task<int> ExecuteAsync(CancellationToken cancellationToken)
	{
		// var uri = GetUri(datFile);
		var uri = _appSettings.ArchiveOrgUri;
		Console.WriteLine(uri);

		string content;
		content = await _httpClient.GetStringAsync(uri, cancellationToken);

		foreach (var datFilePath in Directory.EnumerateFiles(
					_appSettings.DatsRootPath.LocalPath,
					"*.dat",
					SearchOption.TopDirectoryOnly))
		{
			cancellationToken.ThrowIfCancellationRequested();

			var datFile = await _parser.ParseAsync(FileLocation.Create(new Uri(datFilePath)), cancellationToken);

			// var regex = new Regex("<a href=\"(?<url>[^#].+)\">(?<name>[^\\.].+)</a>");
			var regex = new Regex("<a href=\"(?<url>[^\"]+)\">(?<name>[^<]+)</a>");
			var matches = regex.Matches(content);

			// Debug.Assert(matches.Count == 1, "Mismatch input data");

			/*var dataUrl = matches.Single()
				.Groups["url"]
				.Value;

			var dataUri = new Uri($"{uri.AbsoluteUri}/{dataUrl}/".Replace("&", "%26"));
			content = await _httpClient.GetStringAsync(dataUri, cancellationToken);
			matches = regex.Matches(content);*/

			foreach (var game in datFile.Games.Skip(0))
			{
				Console.WriteLine(game.Name);
				foreach (var match in matches.Where(
							a => a.Groups["name"]
								.Value.StartsWith(game.Name) || a.Groups["name"]
								.Value.Replace("&amp;", "&")
								.StartsWith(game.Name)))
				{
					// var fileUrl = new Uri($"https:{match.Groups["url"].Value}");
					var fileUrl = new Uri(
						$"{_appSettings.ArchiveOrgUri}{match.Groups["url"].Value}".Replace(" ", "%20")
							.Replace("&", "%26"));
					Console.WriteLine(fileUrl.AbsoluteUri);

					var fileName = Path.Combine(
						_appSettings.DownloadPath.LocalPath,
						match.Groups["name"]
							.Value);

					if (!Directory.Exists(Path.GetDirectoryName(fileName)))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(fileName) !);
					}
					else if (File.Exists(fileName))
					{
						continue;
					}

					try
					{
						using var response = await _httpClient.GetAsync(
							fileUrl,
							HttpCompletionOption.ResponseHeadersRead,
							cancellationToken);
						await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

						await using var file = File.Create(fileName);
						await stream.CopyToAsync(file, cancellationToken);
						await file.FlushAsync(cancellationToken);
						file.Close();
					}
					catch (Exception e)
					{
						Console.WriteLine(e);

						// throw;
					}
				}
			}
		}

		return 0;
	}

	private Uri GetUri(DataFile datFile)
	{
		var path = datFile.Name.Replace("fix_", "")
			.Replace("Nintendo ", "Nintendo\\")
			.Replace("Commodore ", "Commodore\\")
			.Replace(" - ", "\\")
			.Replace("&", "26%");
		return new Uri(_appSettings.ArchiveOrgUri, path); // .Replace(' ', '\\'));
	}
}
