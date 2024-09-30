using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Application.Users.Commands;

public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, UserId>
{
	private readonly IUserRepository _userRepository;

	public CreateUserCommandHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public override Task<UserId> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
	{
		var model = User.Create(command.UserId, command.Name);

		_userRepository.Add(model);

		return Task.FromResult(model.Id);
	}
}
