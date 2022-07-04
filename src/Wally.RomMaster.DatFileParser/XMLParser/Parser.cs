using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.XMLParser;

public class Parser<TRoot> where TRoot : class
{
	private readonly XmlSerializer _serializer;
	private readonly XmlReaderSettings _settings;

	public Parser()
	{
		_settings = new XmlReaderSettings
		{
			DtdProcessing = DtdProcessing.Parse,
			ValidationType = ValidationType.None,
			MaxCharactersFromEntities = 1024,
		};

		_serializer = new XmlSerializer(typeof(TRoot));
		_serializer.UnknownAttribute += SerializerOnUnknownAttribute;
		_serializer.UnknownElement += SerializerOnUnknownElement;
		_serializer.UnknownNode += SerializerOnUnknownNode;
		_serializer.UnreferencedObject += SerializerOnUnreferencedObject;
	}

	private void SerializerOnUnreferencedObject(object? sender, UnreferencedObjectEventArgs e)
	{
		throw new NotImplementedException();
	}

	private void SerializerOnUnknownNode(object? sender, XmlNodeEventArgs e)
	{
		switch (e.Name)
		{
			case "clrmamepro":
				return;
			default:
				Debugger.Break();
				return; // throw new System.NotImplementedException();
		}
	}

	private void SerializerOnUnknownElement(object? sender, XmlElementEventArgs e)
	{
		// throw new System.NotImplementedException();
	}

	private void SerializerOnUnknownAttribute(object? sender, XmlAttributeEventArgs e)
	{
		throw new NotImplementedException();
	}

	public TRoot Parse(string filePathName)
	{
		using var stream = new FileStream(filePathName, FileMode.Open);

		stream.Seek(0, SeekOrigin.Begin);
		return Parse(stream);
	}

	public TRoot Parse(Stream stream)
	{
		using var reader = XmlReader.Create(stream, _settings);
		return (TRoot)_serializer.Deserialize(reader) !;
	}
}
