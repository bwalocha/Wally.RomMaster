using System;
using System.Collections.Generic;
using System.Diagnostics;

using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Domain.Files;

[DebuggerDisplay("{Name}")]
public class Path : AggregateRoot
{
	private readonly List<File> _files = new();

	// Hide public .ctor
#pragma warning disable CS8618
	private Path()
#pragma warning restore CS8618
	{
	}

	private Path(Path? parent, string name)
	{
		Parent = parent;
		ParentId = parent?.Id;
		Name = name;
	}

	public Guid? ParentId { get; private set; }

	public string Name { get; private set; }

	public IReadOnlyCollection<File> Files => _files.AsReadOnly();

	public Path? Parent { get; private set; }

	public static Path Create(Path? parent, string name)
	{
		var model = new Path(parent, name);

		return model;
	}

	public Path Update(IClockService clockService)
	{
		ModifiedAt = clockService.GetTimestamp();
		
		return this;
	}
}
