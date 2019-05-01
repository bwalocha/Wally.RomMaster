using System.Collections.Generic;

namespace Wally.RomMaster.Domain.Models
{
    public class AppSettings
    {
        public List<Folder> DatRoots { get; } = new List<Folder>();

        public List<Folder> RomRoots { get; } = new List<Folder>();

        public List<Folder> ToSortRoots { get; } = new List<Folder>();
    }
}
