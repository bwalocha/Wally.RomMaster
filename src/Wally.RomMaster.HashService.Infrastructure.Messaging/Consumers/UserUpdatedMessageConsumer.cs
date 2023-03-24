using System.Threading.Tasks;

using MassTransit;

using MediatR;

using Wally.Identity.Messages.Users;
using Wally.RomMaster.HashService.Application.Users.Commands;

namespace Wally.RomMaster.HashService.Infrastructure.Messaging.Consumers;

public class UserUpdatedMessageConsumer : IConsumer<UserUpdatedMessage>
{
	private readonly IMediator _mediator;

	public UserUpdatedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task Consume(ConsumeContext<UserUpdatedMessage> context)
	{
		var message = context.Message;
		var command = new UpdateUserCommand(message.UserId, message.UserName);

		return _mediator.Send(command);
	}
}
