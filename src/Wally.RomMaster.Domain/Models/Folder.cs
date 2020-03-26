using System.Collections.Generic;
using System.IO;

namespace Wally.RomMaster.Domain.Models
{
    public class Folder
    {
        public bool Enabled { get; set; } = true;

        public string Path { get; set; } = string.Empty;

        public SearchOption SearchOptions { get; set; } = SearchOption.TopDirectoryOnly;

        public bool WatcherEnabled { get; set; } = false;

        public List<Exclude> Excludes { get; } = new List<Exclude>();

        public override string ToString()
        {
            return Path;
        }
    }
}
