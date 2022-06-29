using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using Wally.RomMaster.DatFileParser.Models;

// using System.Xml.Schema;

namespace Wally.RomMaster.DatFileParser.LogiqxXMLParser;

public class Parser
{
	private readonly XmlSerializer _serializer;
	private readonly XmlReaderSettings _settings;

	public Parser()
	{
		_settings = new XmlReaderSettings
		{
			// settings.DtdProcessing = DtdProcessing.Ignore;
			DtdProcessing = DtdProcessing.Parse,
			ValidationType = ValidationType.None,

			// settings.ValidationType = ValidationType.DTD;
			MaxCharactersFromEntities = 1024,
		};

		_settings.ValidationEventHandler += ValidationCallBack;

		_serializer = new XmlSerializer(typeof(DataFile), new XmlRootAttribute("datafile"));
	}

	public DataFile Parse(string filePathName)
	{
		using var stream = new FileStream(filePathName, FileMode.Open);
		Validate(stream);

		stream.Seek(0, SeekOrigin.Begin);
		return Parse(stream);
	}

	public DataFile Parse(Stream stream)
	{
		using var reader = XmlReader.Create(stream, _settings);
		return (DataFile)_serializer.Deserialize(reader);
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

	private static void ValidationCallBack(object sender, ValidationEventArgs e)
	{
		System.Console.WriteLine("Validation Error: {0}", e.Message);
	}
}
