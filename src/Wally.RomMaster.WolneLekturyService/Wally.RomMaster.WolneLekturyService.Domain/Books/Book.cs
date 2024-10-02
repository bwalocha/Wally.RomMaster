/*
using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class Book : AggregateRoot<Book, BookId>, ISoftDeletable
{
	// Hide public .ctor
#pragma warning disable CS8618
	private Book()
#pragma warning restore CS8618
	{
	}

	private Book(BookId id, string name)
		: base(id)
	{
		Name = name;
	}

	private Book(string name)
	{
		Name = name;
	}

	public string Name { get; private set; }

	public bool IsDeleted { get; private set; } = false;
	public DateTimeOffset? DeletedAt { get; private set; } = null;

	public UserId? DeletedById { get; private set; } = null;

	public static Book Create(string name)
	{
		var model = new Book(name);
		model.AddDomainEvent(new UserCreatedDomainEvent(model.Id, model.Name));

		return model;
	}

	public static Book Create(UserId id, string name)
	{
		var model = new Book(id, name);
		model.AddDomainEvent(new UserCreatedDomainEvent(model.Id, model.Name));

		return model;
	}

	public Book Update(string name)
	{
		Name = name;

		return this;
	}
}
*/
