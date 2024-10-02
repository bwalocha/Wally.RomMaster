using MediatR;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Application.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
	where TResult : IResponse
{
}
