using MediatR;

using Microsoft.Extensions.Logging;

using Wally.IdentityProvider.Contracts.Messages;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.HashService.Application.Users.Commands;

namespace Wally.RomMaster.HashService.Messaging.Consumers;

public class UserCreatedMessageConsumer : Consumer<UserCreatedMessage>
{
	public UserCreatedMessageConsumer(IMediator mediator, ILogger<UserCreatedMessageConsumer> logger)
		: base(mediator, logger)
	{
	}

	protected override ICommand CreateCommand(UserCreatedMessage message)
	{
		return new CreateUserCommand(message.UserId, message.UserName);
	}
}
