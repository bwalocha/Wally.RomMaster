using System.Collections.Generic;

using Wally.Lib.DDD.Abstractions.DomainModels;

namespace Wally.RomMaster.Domain.Files
{
	public class Game : Entity
	{
		// Hide public .ctor
#pragma warning disable CS8618
		private Game()
#pragma warning restore CS8618
		{
		}
		
		public string Name { get; private set; }

		public string Description { get; private set; }

		public string Year { get; private set; }

		private readonly List<Rom> _roms = new();

		public IReadOnlyCollection<Rom> Roms => _roms.AsReadOnly();
	}
}
