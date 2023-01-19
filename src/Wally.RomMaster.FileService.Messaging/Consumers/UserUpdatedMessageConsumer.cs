using MediatR;

using Microsoft.Extensions.Logging;

using Wally.IdentityProvider.Contracts.Messages;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.FileService.Application.Users.Commands;

namespace Wally.RomMaster.FileService.Messaging.Consumers;

public class UserUpdatedMessageConsumer : Consumer<UserUpdatedMessage>
{
	public UserUpdatedMessageConsumer(IMediator mediator, ILogger<UserUpdatedMessageConsumer> logger)
		: base(mediator, logger)
	{
	}

	protected override ICommand CreateCommand(UserUpdatedMessage message)
	{
		return new UpdateUserCommand(message.UserId, message.UserName);
	}
}
