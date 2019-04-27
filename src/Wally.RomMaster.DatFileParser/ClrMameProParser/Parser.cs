namespace Wally.RomMaster.DatFileParser.ClrMameProParser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    internal class Parser
    {
        public Parser()
        {
        }

        public async Task<Models.DataFile> ParseAsync(string filePathName)
        {
            using (var stream = new FileStream(filePathName, FileMode.Open))
            {
                return await ParseAsync(stream).ConfigureAwait(false);
            }
        }

        public async Task<Models.DataFile> ParseAsync(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                var enumerator = new AsyncLineEnumerator(reader);

                var header = await ReadHeaderAsync(enumerator).ConfigureAwait(false);
                var games = await this.ReadGamesAsync(enumerator).ConfigureAwait(false);

                return new Models.DataFile
                {
                    Header = header,
                    Games = games
                };
            }
        }

        private static async Task<Models.Header> ReadHeaderAsync(IAsyncEnumerator<string> lines)
        {
            var header = new Models.Header();

            if (!await lines.MoveNextAsync())
            {
                throw new ArgumentException("Unknown input data format");
            }

            var line = lines.Current;

            if (line != "clrmamepro (")
            {
                throw new ArgumentOutOfRangeException(nameof(lines), line, $"Unexpected value");
            }

            while (await lines.MoveNextAsync())
            {
                line = lines.Current.Trim();

                var tags = line.Split(' ', 2);

                var key = tags[0];

                if (key == ")")
                {
                    break;
                }

                var value = tags.Length > 1 ? tags[1].Trim('"') : null;
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

        private async Task<List<Models.Game>> ReadGamesAsync(IAsyncEnumerator<string> lines)
        {
            var games = new List<Models.Game>();

            while (await lines.MoveNextAsync())
            {
                var game = await this.ReadGameAsync(lines).ConfigureAwait(false);

                if (game != null)
                {
                    games.Add(game);
                }
                else
                {
                    // resource?
                    System.Diagnostics.Debugger.Break();
                    continue;
                }
            }

            return games;
        }

        private async Task<Models.Game> ReadGameAsync(IAsyncEnumerator<string> lines)
        {
            var line = lines.Current;

            if (line.StartsWith("resource", StringComparison.InvariantCulture))
            {
                while (await lines.MoveNextAsync())
                {
                    line = lines.Current;

                    var tags = line.Split(' ', 2);

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
                throw new ArgumentOutOfRangeException(nameof(lines), line, $"Unexpected value");
            }

            var game = new Models.Game();

            while (await lines.MoveNextAsync())
            {
                line = lines.Current;

                var tags = line.Split(' ', 2);

                var key = tags[0];

                if (key == ")")
                {
                    break;
                }

                var value = tags[1].Trim('"');

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
                        game.Roms.Add(await ReadRomAsync(lines).ConfigureAwait(false)); // = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(key), key, "Unexpected token");
                }
            }

            return game;
        }

        private static Task<Models.Rom> ReadRomAsync(IAsyncEnumerator<string> lines)
        {
            var line = lines.Current;
            var tags = line.Split(' ', 2);
            var key = tags[0];

            if (key != "rom")
            {
                throw new ArgumentOutOfRangeException(nameof(lines), line, $"Unexpected value");
            }

            var value = tags[1].TrimStart('(').TrimEnd(')').Trim();
            tags = value.Split(' ', 2);

            var rom = new Models.Rom();

            while (key != ")")
            {
                key = tags[0];
                value = tags[1].StartsWith('"') ? tags[1].Substring(1, tags[1].IndexOf('"', 1)).Trim('"') : tags[1].Split(' ', 2)[0];

                switch (key)
                {
                    case "name":
                        rom.Name = value;
                        break;
                    case "size":
                        rom.Size = uint.Parse(value, System.Globalization.NumberStyles.None);
                        break;
                    case "crc":
                        rom.Crc = value;
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
                    default:
                        throw new ArgumentOutOfRangeException(nameof(key), key, "Unexpected token");
                }

                if (value.Length + (tags[1].StartsWith('"') ? 3 : 1) >= tags[1].Length)
                {
                    break;
                }

                tags = tags[1].Substring(value.Length + (tags[1].StartsWith('"') ? 3 : 1)).Split(' ', 2);
            }

            return Task.FromResult(rom);
        }

        private class AsyncLineEnumerator : IAsyncEnumerator<string>
        {
            private readonly StreamReader reader;

            public AsyncLineEnumerator(StreamReader reader)
            {
                this.reader = reader;
            }

            public string Current { get; private set; }

            public ValueTask DisposeAsync()
            {
                return default;
            }

            public async ValueTask<bool> MoveNextAsync()
            {
                if (this.reader.EndOfStream)
                {
                    return false;
                }

                this.Current = (await this.reader.ReadLineAsync().ConfigureAwait(false)).Trim();
                if (string.IsNullOrWhiteSpace(this.Current))
                {
                    return await this.MoveNextAsync();
                }

                return true;
            }
        }
    }
}
