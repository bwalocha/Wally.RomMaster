using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;

/*
<?xml version="1.0" encoding="utf-8"?>
<retrobytes>
	<header>
		<name>RetroBytes - Game disks</name>
		<description>RetroBytes.eu - Game disks</description>
		<date>2022-06-24 18:05:15 UTC</date>
		<url>https://retrobytes.eu/</url>
	</header>
	<game name="000001_hannibal">
		<description>Hannibal (MicroLeague, 1993)[de][4 x 720Kb][img][Verified]</description>
		<disks>4 x 3.5" DD (720kB)</disks>
		<language>German</language>
		<year>1993</year>
		<protection>Manual</protection>
		<status>Verified</status>
		<format>Binary (.img)</format>
		<rom name="disk1.img" sha1="6e8ae11787f9f3528ab9cc98d56da3f5bc7447bc" size="737280"/>
		<rom name="disk2.img" sha1="7617f6270ac27fb48fa3cc306f0d3cf6e3281428" size="737280"/>
		<rom name="disk3.img" sha1="4562adbdd0da051378e431de8fa16477c7928c03" size="737280"/>
		<rom name="disk4.img" sha1="566275277b8fbeeabb0095aaa428bb2241a82140" size="737280"/>
	</game>
	...
*/

[Serializable]
[XmlRoot(ElementName = "retrobytes")]
public class RetroBytesFile
{
	[XmlElement("header")]
	public Header Header { get; set; }

	[XmlElement("game")]
	public List<Game> Games { get; } = new();
}
