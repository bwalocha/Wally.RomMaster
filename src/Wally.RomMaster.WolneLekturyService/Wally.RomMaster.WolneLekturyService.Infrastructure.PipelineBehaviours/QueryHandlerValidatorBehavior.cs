using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.PipelineBehaviours;

public class QueryHandlerValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IQuery<TResponse>
	where TResponse : IResponse
{
	private readonly IValidator<TRequest>? _validator;

	public QueryHandlerValidatorBehavior(IValidator<TRequest>? validator = null)
	{
		_validator = validator;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		if (_validator != null)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
			{
				throw new ValidationException(validationResult.Errors);
			}
		}

		return await next();
	}
}
