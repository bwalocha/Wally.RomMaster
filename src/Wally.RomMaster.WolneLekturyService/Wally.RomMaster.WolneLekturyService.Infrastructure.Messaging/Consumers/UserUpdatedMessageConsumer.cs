using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Wally.RomMaster.WolneLekturyService.Application.Users.Commands;
using Wally.RomMaster.WolneLekturyService.Domain.Users;
using Wally.Identity.Messages.Users;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.Messaging.Consumers;

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
