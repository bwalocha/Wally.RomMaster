using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;
/*

	<game name="a51mxr3k" cloneof="area51mx" romof="area51mx">
		<description>Area 51 / Maximum Force Duo (R3000)</description>
		<year>1998</year>
		<manufacturer>Atari Games</manufacturer>
		<disk name="area51mx" merge="area51mx" sha1="5ff10f4e87094d4449eabf3de7549564ca568c7e"/>
	</game>

*/

[Serializable]
public class Game
{
	[XmlAttribute("name")]
	public string Name { get; set; } // Air Conflicts - Aces of World War II (USA)

	[XmlAttribute("sourcefile")]
	public string SourceFile { get; set; } // <game name="forgottn" sourcefile="cps1.c">

	[XmlAttribute("cloneof")]
	public string CloneOf { get; set; } // area51mx

	[XmlAttribute("romof")]
	public string RomOf { get; set; } // area51mx

	[XmlElement("description")]
	public string Description { get; set; } // Air Conflicts - Aces of World War II (USA)

	[XmlElement("year")]
	public string Year { get; set; } // 1998, 200?

	[XmlElement("manufacturer")]
	public string Manufacturer { get; set; } // Atari Games

	[XmlElement("disk")]
	public Disk Disk { get; set; }

	[XmlElement("rom")]
	public List<Rom> Roms { get; } = new();

	[XmlAttribute("serial")]
	public string Serial { get; set; }

	[XmlAttribute("region")]
	public string Region { get; set; } // USA

	[XmlElement("game_id")]
	public string GameId { get; set; } // <game_id>RX112</game_id>

	[XmlAttribute("isbios")]
	public string IsBios { get; set; } // <game name="aurora" isbios="yes">

	[XmlAttribute("board")]
	public string Board { get; set; } // <game name="ainferno" board="C45">

	[XmlElement("biosset")]
	public List<BiosSet> BiosSets { get; set; } // <biosset name="bios0" description="epr-21576h (Japan)"/>
}
