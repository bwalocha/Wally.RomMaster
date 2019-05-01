using System.Collections.Generic;
using System.IO;

namespace Wally.RomMaster.Domain.Models
{
    public class Folder
    {
        public bool Enabled { get; set; } = true;

        public string Path { get; set; }

        public SearchOption SearchOptions { get; set; } = SearchOption.TopDirectoryOnly;

        public bool WatcherEnabled { get; set; }

        public List<Exclude> Excludes { get; } = new List<Exclude>();
    }
}
