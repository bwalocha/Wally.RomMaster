﻿using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.NotificationService.Application.Abstractions;

namespace Wally.RomMaster.NotificationService.Application.Users.Commands;

public class DeleteUserCommandHandler : CommandHandler<DeleteUserCommand>
{
	private readonly IUserRepository _userRepository;

	public DeleteUserCommandHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public override async Task HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken)
	{
		var model = await _userRepository.GetAsync(command.UserId, cancellationToken);

		_userRepository.Remove(model);
	}
}
