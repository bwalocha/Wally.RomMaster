using MediatR;

namespace Wally.RomMaster.HashService.Application.Abstractions;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}
