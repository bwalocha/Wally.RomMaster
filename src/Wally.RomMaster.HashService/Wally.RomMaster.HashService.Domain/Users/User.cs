﻿using Wally.RomMaster.HashService.Domain.Abstractions;

namespace Wally.RomMaster.HashService.Domain.Users;

public class User : AggregateRoot<User, UserId>
{
	// Hide public .ctor
#pragma warning disable CS8618
	private User()
#pragma warning restore CS8618
	{
	}

	private User(UserId id, string name)
		: base(id)
	{
		Name = name;
	}

	private User(string name)
	{
		Name = name;
	}

	public string Name { get; private set; }

	public static User Create(string name)
	{
		var model = new User(name);
		model.AddDomainEvent(new UserCreatedDomainEvent(model.Id, model.Name));

		return model;
	}

	public static User Create(UserId id, string name)
	{
		var model = new User(id, name);
		model.AddDomainEvent(new UserCreatedDomainEvent(model.Id, model.Name));

		return model;
	}

	public User Update(string name)
	{
		Name = name;

		return this;
	}
}
