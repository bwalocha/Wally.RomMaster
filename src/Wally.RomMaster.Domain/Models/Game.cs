using System.Collections.Generic;

namespace Wally.RomMaster.Domain.Models
{
    public class Game
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Year { get; set; }

        public virtual ISet<Rom> Roms { get; set; } = new HashSet<Rom>();
    }
}
