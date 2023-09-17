using System.Threading.Tasks;

using MassTransit;

using MediatR;

using Wally.RomMaster.HashService.Application.Users.Commands;
using Wally.Identity.Messages.Users;

namespace Wally.RomMaster.HashService.Infrastructure.Messaging.Consumers;

public class UserCreatedMessageConsumer : IConsumer<UserCreatedMessage>
{
	private readonly ISender _mediator;

	public UserCreatedMessageConsumer(ISender mediator)
	{
		_mediator = mediator;
	}

	public Task Consume(ConsumeContext<UserCreatedMessage> context)
	{
		var message = context.Message;
		var command = new CreateUserCommand(message.UserId, message.UserName);

		return _mediator.Send(command);
	}
}
