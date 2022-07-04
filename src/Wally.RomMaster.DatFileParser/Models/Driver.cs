using System;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;
/*

	<device_ref name="z80"/>

*/

[Serializable]
public class Driver
{
	[XmlAttribute("status")]
	public string Status { get; set; } // <driver status="imperfect"/>
}
