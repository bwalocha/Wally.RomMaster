namespace Wally.RomMaster.DatFileParser
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    // using System.Xml.Schema;
    using System.Xml.Serialization;

    public class Parser
    {
        private readonly XmlReaderSettings settings;
        // private readonly XmlSerializer serializer;

        public Parser()
        {
        }
        
        public async Task<Models.DataFile> ParseAsync(string filePathName)
        {
            using (var stream = new FileStream(filePathName, FileMode.Open))
            {
                return await ParseAsync(stream);
            }
        }

        public async Task<Models.DataFile> ParseAsync(Stream stream)
        {
            try
            {
                return new LogiqxXMLParser.Parser().Parse(stream);
            }
            catch
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            try
            {
                return await new ClrMameProParser.Parser().ParseAsync(stream);
            }
            catch
            {
                // stream.Seek(0, SeekOrigin.Begin);
            }

            throw new ArgumentException();
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

            settings.Schemas.Add(null, XmlReader.Create("datafile.dtd", dtdSettings));

            using (var dtdStream = new FileStream("datafile.dtd", FileMode.Open, FileAccess.Read))
            using (XmlReader reader = XmlReader.Create(stream, settings))
            {
                // XmlSchema schema = XmlSchema.Read(dtdStream, ValidationCallBack);

                //XmlDocument doc = new XmlDocument();

                //doc.Schemas.Add(schema);
                //doc.Schemas.Compile();

                // doc.Load(reader);

                // doc.Validate(ValidationCallBack);

                while (reader.Read());
            }
        }
    }
}
