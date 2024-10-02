using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Users;

[ExcludeFromCodeCoverage]
public class UserCreatedDomainEvent : DomainEvent
{
	public UserCreatedDomainEvent(UserId id, string name)
	{
		Id = id;
		Name = name;
	}

	public UserId Id { get; }

	public string Name { get; }
}
