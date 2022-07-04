using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Wally.RomMaster.DatFileParser.Models;
using Wally.RomMaster.DatFileParser.XMLParser;
using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.Files;

using DataFile = Wally.RomMaster.Domain.DataFiles.DataFile;

namespace Wally.RomMaster.DatFileParser;

public class Parser : IDataFileParser
{
	private readonly IMapper _mapper;

	public Parser(IMapper mapper)
	{
		_mapper = mapper;
	}

	public async Task<DataFile> ParseAsync(FileLocation location, CancellationToken cancellationToken)
	{
		try
		{
			await using var stream = new FileStream(location.Location.LocalPath, FileMode.Open, FileAccess.Read);
			var data = await ParseAsync(stream, cancellationToken);
			return _mapper.Map<DataFile>(data);
		}
		catch
		{
			Debugger.Break();
			throw;
		}
	}

	private static async Task<object> ParseAsync(Stream stream, CancellationToken cancellationToken)
	{
		if (stream == null)
		{
			throw new ArgumentNullException(nameof(stream));
		}

		using var reader = new StreamReader(stream);
		var line = await reader.ReadLineAsync();
		stream.Seek(0, SeekOrigin.Begin);

		if (string.IsNullOrWhiteSpace(line))
		{
			throw new ArgumentException("Unknown DAT file format");
		}

		if (line.StartsWith("<", StringComparison.InvariantCulture))
		{
			while (line.StartsWith("<?") || line.StartsWith("<!") || string.IsNullOrWhiteSpace(line))
			{
				line = await reader.ReadLineAsync();
			}

			if (line.StartsWith("<datafile>"))
			{
				return new Parser<Models.DataFile>().Parse(stream);
			}

			if (line.StartsWith("<datfile>"))
			{
				return new Parser<DatFile>().Parse(stream);
			}

			if (line.StartsWith("<retrobytes>"))
			{
				return new Parser<RetroBytesFile>().Parse(stream);
			}

			throw new ArgumentException($"Unknown DAT file header: '{line}'.");
		}

		if (line.StartsWith("clrmamepro", StringComparison.InvariantCulture))
		{
			return await new ClrMameProParser.Parser().ParseAsync(stream, cancellationToken);
		}

		throw new ArgumentException($"Unknown DAT file header: '{line}'.");
	}
}
