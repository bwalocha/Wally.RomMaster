using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.Application.Files.Commands;

public class RemoveOutdatedFilesCommandHandler : CommandHandler<RemoveOutdatedFilesCommand>
{
	public RemoveOutdatedFilesCommandHandler()
	{
	}

	public override Task HandleAsync(RemoveOutdatedFilesCommand command, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
