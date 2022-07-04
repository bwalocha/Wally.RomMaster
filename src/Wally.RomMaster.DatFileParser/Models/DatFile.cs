using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;

/*
<datafile>
	<header>
		<name>Commodore Amiga - Compilations - Applications</name>
		<description>Commodore Amiga - Compilations - Applications (TOSEC-v2021-07-31)</description>
		<category>TOSEC</category>
		<version>2021-07-31</version>
		<date>2021-07-31</date>
		<author>BippyM - Dpainter - LaRock - Sea7 - Wattie - mai - Crashdisk</author>
		<email>contact@tosecdev.org</email>
		<homepage>TOSEC</homepage>
		<url>www.tosecdev.org</url>
		<comment>...to be continued!</comment>
		<clrmamepro/>
	</header>
	<game name="10000-Watts (1987)(ACSW - Blade)">
		<description>10000-Watts (1987)(ACSW - Blade)</description>
		<rom name="10000-Watts (1987)(ACSW - Blade).adf" size="901120" crc="b1e40889" md5="c6567c343d67e90c23dd0f6d8d7d3df3" sha1="ca6e33faed9a6cd22fedbb02f45e9955d1e557c3"/>
	</game>
*/
/*
<?xml version="1.0"?>
<DatFile>
	<header>
		<name>MAME V0.242 CHDs (merged)</name>
		<description>MAME V0.242 CHDs (merged)</description>
		<romvault/>
	</header>
	<game name="2spicy" romof="lindbios">
		<description>2 Spicy</description>
		<year>2007</year>
		<manufacturer>Sega</manufacturer>
		<disk name="dvp-0027a.chd" sha1="da1aacee9e32e813844f4d434981e69cc5c80682"/>
	</game> 
*/

[Serializable]
[XmlRoot(ElementName = "DatFile")]
public class DatFile
{
	[XmlElement("header")]
	public Header Header { get; set; }

	[XmlElement("game")]
	public List<Game> Games { get; } = new();
}
