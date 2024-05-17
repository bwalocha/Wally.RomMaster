using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Wally.RomMaster.FileService.Application.Users.Commands;
using Wally.RomMaster.FileService.Domain.Users;
using Wally.Identity.Messages.Users;

namespace Wally.RomMaster.FileService.Infrastructure.Messaging.Consumers;

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
		var command = new CreateUserCommand(new UserId(message.UserId), message.UserName);

		return _mediator.Send(command);
	}
}
