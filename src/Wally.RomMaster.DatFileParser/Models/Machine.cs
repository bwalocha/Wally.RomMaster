using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;
/*

	<machine name="bosses">
		<description>bosses</description>
		<rom name="akumajou.png" size="9725" crc="ef79cc9b" sha1="77c0158efa8eaf670961bd2def0daf30594f20df"/>
		<rom name="aquajacku.png" size="27447" crc="35246486" sha1="775e238000be9d1dacfaa180fbf8f779f816ba32"/>
		<rom name="area88.png" size="121563" crc="68788868" sha1="2502e211d4d88ccc4c84e8a3f6b1428ddba7a353"/>
		<rom name="tdragonb2.png" size="21538" crc="ed4fe535" sha1="a16929b8142f79956d870456685ce2b2b6d59d30"/>
		<rom name="tdragonb3.png" size="24272" crc="843de96d" sha1="522de0b6090b9fb64f8dbc72ee3b221bc72342bd"/>
	</machine>

*/

[Serializable]
public class Machine
{
	[XmlAttribute("name")]
	public string Name { get; set; } // Air Conflicts - Aces of World War II (USA)

	[XmlElement("description")]
	public string Description { get; set; } // Air Conflicts - Aces of World War II (USA)

	[XmlElement("rom")]
	public List<Rom> Roms { get; } = new();
}
