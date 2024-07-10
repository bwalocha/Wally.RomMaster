using MediatR;
using Wally.RomMaster.HashService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.HashService.Application.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
	where TResult : IResponse
{
}
