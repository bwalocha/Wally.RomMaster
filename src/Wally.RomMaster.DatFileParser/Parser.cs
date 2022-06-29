using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using AutoMapper;

using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.DataFiles;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.DatFileParser;

public class Parser : IDataFileParser
{
	private readonly IMapper _mapper;
	private readonly XmlReaderSettings _settings;

	public Parser(IMapper mapper)
	{
		_mapper = mapper;
		_settings = new XmlReaderSettings();
	}

	public async Task<DataFile> ParseAsync(FileLocation location, CancellationToken cancellationToken)
	{
		await using var stream = new FileStream(location.Location.LocalPath, FileMode.Open, FileAccess.Read);
		var data = await ParseAsync(stream, cancellationToken);
		return _mapper.Map<DataFile>(data);
	}

	private static async Task<Models.DataFile> ParseAsync(Stream stream, CancellationToken cancellationToken)
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
			return new LogiqxXMLParser.Parser().Parse(stream);
		}

		if (line.StartsWith("clrmamepro", StringComparison.InvariantCulture))
		{
			return await new ClrMameProParser.Parser().ParseAsync(stream, cancellationToken);
		}

		throw new ArgumentException($"Unknown DAT file header: '{line}'.");
	}

	public void Validate(Stream stream)
	{
		var dtdSettings = new XmlReaderSettings
		{
			// settings.DtdProcessing = DtdProcessing.Ignore;
			DtdProcessing = DtdProcessing.Parse,
		};

		// dtdSettings.ValidationType = ValidationType.DTD;
		// dtdSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

		_settings.Schemas.Add(null, XmlReader.Create("datafile.dtd", dtdSettings));

		using var dtdStream = new FileStream("datafile.dtd", FileMode.Open, FileAccess.Read);
		using var reader = XmlReader.Create(stream, _settings);

		// XmlSchema schema = XmlSchema.Read(dtdStream, ValidationCallBack);

		// XmlDocument doc = new XmlDocument();

		// doc.Schemas.Add(schema);
		// doc.Schemas.Compile();

		// doc.Load(reader);

		// doc.Validate(ValidationCallBack);

		while (reader.Read())
		{
		}
	}
}
