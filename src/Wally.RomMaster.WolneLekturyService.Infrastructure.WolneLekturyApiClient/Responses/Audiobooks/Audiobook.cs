/*
using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Audiobooks;

public class Audiobook : AggregateRoot<Audiobook, AudiobookId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Audiobook()
#pragma warning restore CS8618
	{
	}

	private Audiobook(AudiobookId id, string name)
		: base(id)
	{
		Name = name;
	}

	private Audiobook(string name)
	{
		Name = name;
	}

	public string Name { get; private set; }

	public bool IsDeleted { get; private set; } = false;
	public DateTimeOffset? DeletedAt { get; private set; } = null;

	public UserId? DeletedById { get; private set; } = null;

	public static Audiobook Create(string name)
	{
		var model = new Audiobook(name);
		model.AddDomainEvent(new UserCreatedDomainEvent(model.Id, model.Name));

		return model;
	}

	public static Audiobook Create(UserId id, string name)
	{
		var model = new Audiobook(id, name);
		model.AddDomainEvent(new UserCreatedDomainEvent(model.Id, model.Name));

		return model;
	}

	public Audiobook Update(string name)
	{
		Name = name;

		return this;
	}
}
*/
