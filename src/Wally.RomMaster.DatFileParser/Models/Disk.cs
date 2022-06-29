using System;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models;
/*

	<game name="a51mxr3k" cloneof="area51mx" romof="area51mx">
		<description>Area 51 / Maximum Force Duo (R3000)</description>
		<year>1998</year>
		<manufacturer>Atari Games</manufacturer>
		<disk name="area51mx" merge="area51mx" sha1="5ff10f4e87094d4449eabf3de7549564ca568c7e"/>
	</game>
	
	<game name="wcombat" romof="kviper">
		<description>World Combat (ver AAD:B)</description>
		<year>2002</year>
		<manufacturer>Konami</manufacturer>
		<disk name="c22d02.chd" sha1="69a24c9e36b073021d55bec27d89fcc0254a60cc"/>
		<disk name="c22d02_alt.chd" sha1="772e3fe7910f5115ec8f2235bb48ba9fcac6950d"/>
		<disk name="c22a02.chd" sha1="7200c7c436491fd8027d6d7139a80ee3b984697b"/>
		<disk name="c22c02.chd" sha1="8bd1dfbf926ad5b28fa7dafd7e31c475325ec569" status="baddump"/>
	</game>

*/

[Serializable]
public class Disk
{
	[XmlAttribute("name")]
	public string Name { get; set; } // area51mx

	[XmlAttribute("merge")]
	public string Merge { get; set; } // area51mx

	[XmlAttribute("sha1")]
	public string Sha1 { get; set; } // 5ff10f4e87094d4449eabf3de7549564ca568c7e
	
	[XmlAttribute("status")]
	public string Status { get; set; }
}
