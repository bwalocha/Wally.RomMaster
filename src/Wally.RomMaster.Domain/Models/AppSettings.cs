namespace Wally.RomMaster.Domain.Models
{
    using System.Collections.Generic;

    public class AppSettings
    {
        public List<Folder> DatRoots { get; } = new List<Folder>();

        public List<Folder> RomRoots { get; } = new List<Folder>();

        public List<Folder> ToSortRoots { get; } = new List<Folder>();
    }
}
