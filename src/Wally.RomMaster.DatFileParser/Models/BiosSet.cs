using System;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;
/*

	<device_ref name="z80"/>

*/

[Serializable]
public class BiosSet
{
	[XmlAttribute("name")]
	public string Name { get; set; } // name="bios0"

	[XmlAttribute("description")]
	public string Description { get; set; } // description="epr-21576h (Japan)"

	[XmlAttribute("default")]
	public string Default { get; set; } //  <biosset name="euro" description="Europe MVS (Ver. 2)" default="yes"/>
}
