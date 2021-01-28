using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Wally.RomMaster.DatFileParser
{
	public class Parser
	{
		private readonly XmlReaderSettings _settings;

		public Parser()
		{
			_settings = new XmlReaderSettings();
		}

		public async Task<Models.DataFile> ParseAsync(string filePathName, CancellationToken cancellationToken)
		{
			using var stream = new FileStream(filePathName, FileMode.Open);
			return await ParseAsync(stream, cancellationToken).ConfigureAwait(false);
		}

		public static async Task<Models.DataFile> ParseAsync(Stream stream, CancellationToken cancellationToken)
		{
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}

			using StreamReader reader = new StreamReader(stream);
			var line = await reader.ReadLineAsync().ConfigureAwait(false);
			stream.Seek(0, SeekOrigin.Begin);

			if (line.StartsWith("<", StringComparison.InvariantCulture))
			{
				return new LogiqxXMLParser.Parser().Parse(stream);
			}

			if (line.StartsWith("clrmamepro", StringComparison.InvariantCulture))
			{
				return await new ClrMameProParser.Parser().ParseAsync(stream, cancellationToken).ConfigureAwait(false);
			}

			throw new ArgumentException($"Unknown DAT file header: '{line}'.");
		}

		public void Validate(Stream stream)
		{
			var dtdSettings = new XmlReaderSettings
							{
								// settings.DtdProcessing = DtdProcessing.Ignore;
								DtdProcessing = DtdProcessing.Parse
							};

			// dtdSettings.ValidationType = ValidationType.DTD;
			// dtdSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

			_settings.Schemas.Add(null, XmlReader.Create("datafile.dtd", dtdSettings));

			using var dtdStream = new FileStream("datafile.dtd", FileMode.Open, FileAccess.Read);
			using XmlReader reader = XmlReader.Create(stream, _settings);

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
}
