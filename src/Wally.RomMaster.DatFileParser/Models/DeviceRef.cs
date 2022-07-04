using System;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;
/*

	<device_ref name="z80"/>

*/

[Serializable]
public class DeviceRef
{
	[XmlAttribute("name")]
	public string Name { get; set; } // Air Conflicts - Aces of World War II (USA)
}
