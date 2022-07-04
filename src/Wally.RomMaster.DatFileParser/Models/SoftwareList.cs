using System;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;
/*

	<softwarelist name="32x"/>

*/

[Serializable]
public class SoftwareList
{
	[XmlAttribute("name")]
	public string Name { get; set; } // <softwarelist name="32x"/>
}
