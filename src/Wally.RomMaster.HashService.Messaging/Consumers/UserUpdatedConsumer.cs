using MediatR;

using Microsoft.Extensions.Logging;

using Wally.IdentityProvider.Contracts.Messages;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.HashService.Application.Users.Commands;

namespace Wally.RomMaster.HashService.Messaging.Consumers;

public class UserUpdatedConsumer : Consumer<UserUpdatedMessage>
{
	public UserUpdatedConsumer(IMediator mediator, ILogger<UserUpdatedConsumer> logger)
		: base(mediator, logger)
	{
	}

	protected override ICommand CreateCommand(UserUpdatedMessage message)
	{
		return new UpdateUserCommand(message.UserId, message.UserName);
	}
}
