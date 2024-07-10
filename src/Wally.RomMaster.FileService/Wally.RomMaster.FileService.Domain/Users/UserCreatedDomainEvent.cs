using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Domain.Users;

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
