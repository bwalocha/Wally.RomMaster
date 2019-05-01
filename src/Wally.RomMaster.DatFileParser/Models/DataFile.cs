using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wally.RomMaster.DatFileParser.Models
{
    [Serializable]
    [XmlRoot(ElementName = "datafile")]
    public class DataFile
    {
        [XmlElement("header")]
        public Header Header { get; set; }
        [XmlElement("game")]
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
