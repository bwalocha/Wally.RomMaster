using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.Lib.DDD.Abstractions.DomainModels;

namespace Wally.RomMaster.PipelineBehaviours;

public class DomainEventHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : ICommand, IRequest<TResponse>
{
	private readonly DbContext _dbContext;
	private readonly ILogger<DomainEventHandlerBehavior<TRequest, TResponse>> _logger;
	private readonly IServiceProvider _serviceProvider;

	public DomainEventHandlerBehavior(
		DbContext dbContext,
		IServiceProvider serviceProvider,
		ILogger<DomainEventHandlerBehavior<TRequest, TResponse>> logger)
	{
		_dbContext = dbContext;
		_serviceProvider = serviceProvider;
		_logger = logger;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		var response = await next();

		var domainEntities = _dbContext.ChangeTracker.Entries<AggregateRoot>()
			.Where(
				e => e.Entity.GetDomainEvents()
					.Any())
			.ToList();

		var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents())
			.ToList();

		var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

		_logger.LogDebug($"Rows affected: '{rowsAffected}'");

		foreach (var domainEvent in domainEvents)
		{
			var domainEvenHandlerType = typeof(IDomainEventHandler<>);
			var domainEvenHandlerTypeWithGenericType = domainEvenHandlerType.MakeGenericType(domainEvent.GetType());

			foreach (dynamic? service in _serviceProvider.GetServices(domainEvenHandlerTypeWithGenericType))
			{
				await service!.HandleAsync((dynamic)domainEvent, cancellationToken);
			}
		}

		return response;
	}
}
