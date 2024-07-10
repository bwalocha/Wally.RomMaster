using MediatR;
using Wally.RomMaster.FileService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.FileService.Application.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
	where TResult : IResponse
{
}
