using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Wally.RomMaster.DatFileParser.Models;

namespace Wally.RomMaster.DatFileParser.ClrMameProParser;

internal class Parser
{
	public async Task<DataFile> ParseAsync(string filePathName, CancellationToken cancellationToken)
	{
		using var stream = new FileStream(filePathName, FileMode.Open);
		return await ParseAsync(stream, cancellationToken);
	}

	public async Task<DataFile> ParseAsync(Stream stream, CancellationToken cancellationToken)
	{
		using var reader = new StreamReader(stream);
		var enumerator = new AsyncLineEnumerator(reader);

		var header = await ReadHeaderAsync(enumerator, cancellationToken);
		var games = ReadGamesAsync(enumerator, cancellationToken);

		var response = new DataFile { Header = header, };

		while (await games.MoveNextAsync()
					.ConfigureAwait(false))
		{
			cancellationToken.ThrowIfCancellationRequested();

			response.Games.Add(games.Current);
		}

		return response;
	}

	private static async Task<Header> ReadHeaderAsync(
		IAsyncEnumerator<string?> lines,
		CancellationToken cancellationToken)
	{
		var header = new Header();

		if (!await lines.MoveNextAsync())
		{
			throw new ArgumentException("Unknown input data format");
		}

		var line = lines.Current;

		if (line == "game (")
		{
			return header;
		}

		if (line != "clrmamepro (")
		{
			throw new ArgumentOutOfRangeException(nameof(lines), line, $"Unexpected value: '{line}'");
		}

		while (await lines.MoveNextAsync())
		{
			cancellationToken.ThrowIfCancellationRequested();

			line = lines.Current!.Trim();

			var tags = line.Split(' ', 2);

			var key = tags[0];

			if (key == ")")
			{
				break;
			}

			var value = tags.Length > 1
				? tags[1]
					.Trim('"')
				: null;
			if (value == null)
			{
				continue;
			}

			switch (key)
			{
				case "name":
					header.Name = value;
					break;
				case "description":
					header.Description = value;
					break;
				case "version":
					header.Version = value;
					break;
				case "comment":
					header.Comment = value;
					break;
				case "category":
					header.Category = value;
					break;
				case "author":
					header.Author = value;
					break;
				case "date":
					header.Date = value;
					break;
				case "email":
					header.Email = value;
					break;
				case "homepage":
					header.HomePage = value;
					break;
				case "url":
					header.Url = value;
					break;
				case "forcemerging":
					header.ForceMerging = value;
					break;
				case "forcezipping":
					header.ForceZipping = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(key), key, "Unexpected token");
			}
		}

		return header;
	}

	private static async IAsyncEnumerator<Game> ReadGamesAsync(
		IAsyncEnumerator<string?> lines,
		CancellationToken cancellationToken)
	{
		while (await lines.MoveNextAsync())
		{
			cancellationToken.ThrowIfCancellationRequested();

			var game = await ReadGameAsync(lines, cancellationToken);

			if (game != null)
			{
				yield return game;
			}
			else
			{
				// resource?
				Debugger.Break();
			}
		}
	}

	private static async Task<Game?> ReadGameAsync(IAsyncEnumerator<string?> lines, CancellationToken cancellationToken)
	{
		var line = lines.Current;

		if (string.IsNullOrWhiteSpace(line))
		{
			return null;
		}

		if (line.StartsWith("resource", StringComparison.InvariantCulture))
		{
			while (await lines.MoveNextAsync())
			{
				cancellationToken.ThrowIfCancellationRequested();

				line = lines.Current;

				var tags = line!.Split(' ', 2);

				var key = tags[0];

				if (key == ")")
				{
					break;
				}
			}

			return null;
		}

		if (line != "game (")
		{
			throw new ArgumentOutOfRangeException(nameof(lines), line, $"Unexpected value: '{line}'");
		}

		var game = new Game();

		while (await lines.MoveNextAsync())
		{
			cancellationToken.ThrowIfCancellationRequested();

			line = lines.Current;

			var tags = line!.Split(' ', 2);

			var key = tags[0];

			if (key == ")")
			{
				break;
			}

			var value = tags[1]
				.Trim('"');

			switch (key)
			{
				case "name":
					game.Name = value;
					break;
				case "description":
					game.Description = value;
					break;
				case "serial":
					game.Serial = value;
					break;
				case "cloneof":
					game.CloneOf = value;
					break;
				case "romof":
					game.RomOf = value;
					break;
				case "year":
					game.Year = value; // int.Parse(value);
					break;
				case "manufacturer":
					game.Manufacturer = value;
					break;
				case "rom":
					game.Roms.Add(await ReadRomAsync(lines, cancellationToken)); // = value;
					break;
				case "region":
					game.Region = value;
					break;
				case "board":
					game.Board = value;
					break;
				case "chip":
					break;
				case "video":
					break;
				case "sound":
					break;
				case "input":
					break;
				case "dipswitch":
					break;
				case "driver":
					break;
				case "sampleof":
					break;
				case "sample":
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(key), key, "Unexpected token");
			}
		}

		return game;
	}

	private static Task<Rom> ReadRomAsync(IAsyncEnumerator<string?> lines, CancellationToken cancellationToken)
	{
		var line = lines.Current;

		if (string.IsNullOrWhiteSpace(line))
		{
			throw new ArgumentOutOfRangeException(nameof(lines), line, "Empty line");
		}

		var tags = line.Split(' ', 2);
		var key = tags[0];

		if (key != "rom")
		{
			throw new ArgumentOutOfRangeException(nameof(lines), line, $"Unexpected value: '{key}'");
		}

		var value = tags[1]
			.TrimStart('(')
			.TrimEnd(')')
			.Trim();
		tags = value.Split(' ', 2);

		var rom = new Rom();

		while (key != ")")
		{
			cancellationToken.ThrowIfCancellationRequested();

			key = tags[0];
			value = tags[1]
				.StartsWith('"')
				? tags[1]
					.Substring(
						1,
						tags[1]
							.IndexOf('"', 1))
					.Trim('"')
				: tags[1]
					.Split(' ', 2)[0];

			switch (key)
			{
				case "name":
					rom.Name = value;
					break;
				case "size":
					rom.Size = value; // uint.Parse(value, NumberStyles.None, CultureInfo.InvariantCulture);
					break;
				case "crc":
					rom.Crc = value.StartsWith("0x") ? value.Substring(2) : value;
					break;
				case "md5":
					rom.Md5 = value;
					break;
				case "sha1":
					rom.Sha1 = value;
					break;
				case "flags":
					rom.Flags = value;
					break;
				case "merge":
					rom.Merge = value;
					break;
				case "serial":
					rom.Serial = value;
					break;
				case "region":
					// rom.Region = value;
					break;
				case "offs":
					rom.Offset = uint.Parse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(key), key, $"Unexpected token: '{key}'");
			}

			if (value.Length + (tags[1]
					.StartsWith('"')
					? 3
					: 1) >= tags[1]
					.Length)
			{
				break;
			}

			tags = tags[1]
				.Substring(
					value.Length + (tags[1]
						.StartsWith('"')
						? 3
						: 1))
				.Split(' ', 2);
		}

		return Task.FromResult(rom);
	}

	private class AsyncLineEnumerator : IAsyncEnumerator<string?>
	{
		private readonly StreamReader _reader;

		public AsyncLineEnumerator(StreamReader reader)
		{
			_reader = reader;
			Current = null;
		}

		public string? Current { get; private set; }

		public ValueTask DisposeAsync()
		{
			return default;
		}

		public async ValueTask<bool> MoveNextAsync()
		{
			if (_reader.EndOfStream)
			{
				return false;
			}

			var line = await _reader.ReadLineAsync();
			Current = line?.Trim();
			if (string.IsNullOrWhiteSpace(Current))
			{
				return await MoveNextAsync();
			}

			return true;
		}
	}
}
