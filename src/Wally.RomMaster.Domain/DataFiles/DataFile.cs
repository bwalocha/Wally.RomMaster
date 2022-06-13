﻿using System;
using System.Collections.Generic;

using Wally.Lib.DDD.Abstractions.DomainModels;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Domain.DataFiles;

/*
	<name>fix_Sony - PlayStation Portable</name>
	<description>fix_</description>
	<category>FIXDATFILE</category>
	<version>06.20.2018 16:42:58</version>
	<date>06.20.2018</date>
	<author>RomVault</author>
*/
public class DataFile : AggregateRoot
{
	private readonly List<Game> _games = new();

	// Hide public .ctor
#pragma warning disable CS8618
	private DataFile()
#pragma warning restore CS8618
	{
	}

	public string Name { get; private set; }

	public string Description { get; private set; }

	public string Category { get; private set; }

	public string Version { get; private set; }

	public DateTime? Date { get; private set; }

	public string Author { get; private set; }

	public string Email { get; private set; }

	public string HomePage { get; private set; }

	public Uri Url { get; private set; }

	public IReadOnlyCollection<Game> Games => _games.AsReadOnly();

	public File File { get; private set; }

	public int FileId { get; private set; }
}