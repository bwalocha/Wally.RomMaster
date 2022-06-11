using System.Collections.Generic;

using Wally.Lib.DDD.Abstractions.DomainModels;

namespace Wally.RomMaster.Domain.DataFiles;

public class Game : Entity
{
	private readonly List<Rom> _roms = new();

	// Hide public .ctor
#pragma warning disable CS8618
	private Game()
#pragma warning restore CS8618
	{
	}

	public string Name { get; private set; }

	public string Description { get; private set; }

	public string Year { get; private set; }

	public IReadOnlyCollection<Rom> Roms => _roms.AsReadOnly();
}
