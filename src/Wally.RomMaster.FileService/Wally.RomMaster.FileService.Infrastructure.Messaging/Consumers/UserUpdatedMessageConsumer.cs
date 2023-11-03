using System.Threading.Tasks;

using MassTransit;

using MediatR;

using Wally.RomMaster.FileService.Application.Users.Commands;
using Wally.RomMaster.FileService.Domain.Users;
using Wally.Identity.Messages.Users;

namespace Wally.RomMaster.FileService.Infrastructure.Messaging.Consumers;

public class UserUpdatedMessageConsumer : IConsumer<UserUpdatedMessage>
{
	private readonly ISender _mediator;

	public UserUpdatedMessageConsumer(ISender mediator)
	{
		_mediator = mediator;
	}

	public Task Consume(ConsumeContext<UserUpdatedMessage> context)
	{
		var message = context.Message;
		var command = new UpdateUserCommand(new UserId(message.UserId), message.UserName);

		return _mediator.Send(command);
	}
}
