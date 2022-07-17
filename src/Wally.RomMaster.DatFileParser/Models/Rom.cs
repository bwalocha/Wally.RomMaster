using System;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;

[Serializable]
public class Rom
{
	[XmlAttribute("name")]
	public string Name { get; set; } // Air Conflicts - Aces of World War II (USA).iso

	[XmlAttribute("size")]
	public string Size { get; set; } // 900825088 // 4453531648

	[XmlAttribute("crc")]
	public string Crc { get; set; } // 1a36c729

	[XmlAttribute("sha1")]
	public string Sha1 { get; set; } // d460e9b82e8c69c78d7c74503e9b9c04df207273

	[XmlAttribute("sha256")]
	public string Sha256 { get; set; } // 8cdfe2b5a992a3606eb92a0536a16dcdd303a7cec3a1daa894959affad306103

	[XmlAttribute("md5")]
	public string Md5 { get; set; } // 6d4e5e80182e438ded1f10eec6265644

	[XmlAttribute("flags")]
	public string Flags { get; set; }

	[XmlAttribute("merge")]
	public string Merge { get; set; }

	[XmlAttribute("status")]
	public string Status { get; set; } // verified

	[XmlAttribute("date")]
	public string Date { get; set; } // date="Wed, 24 Jul 2019 11"

	[XmlAttribute("serial")]
	public string Serial { get; set; } // serial="ZPJ112"

	[XmlAttribute("mia")]
	public string Mia { get; set; } // mia="yes"

	[XmlAttribute("header")]
	public string Header { get; set; } // header="4E 45 53 1A 08 00 21 08 00 00 00 07 00 00 00 01"

	[XmlAttribute("offset")]
	public uint Offset { get; set; } // <rom name="vm2001f01.u17" size="8388608" crc="       0" offset="41943040"/>

	[XmlAttribute("bios")]
	public string Bios { get; set; } // <rom name="dcnaodev.bios" bios="bios26" size="524288" crc="7a50fab9"/>
}
