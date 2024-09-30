using MediatR;
using Wally.RomMaster.NotificationService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.NotificationService.Application.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
	where TResult : IResponse
{
}
