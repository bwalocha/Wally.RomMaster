using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.NotificationService.Domain.Abstractions;

namespace Wally.RomMaster.NotificationService.Domain.Users;

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
