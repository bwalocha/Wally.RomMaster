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
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Infrastructure.PipelineBehaviours;

public class DomainEventHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : ICommand, IRequest<TResponse>
{
	private readonly DbContext _dbContext;
	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger<DomainEventHandlerBehavior<TRequest, TResponse>> _logger;

	public DomainEventHandlerBehavior(DbContext dbContext, IServiceProvider serviceProvider, ILogger<DomainEventHandlerBehavior<TRequest, TResponse>> logger)
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

		var domainEntities = _dbContext.ChangeTracker.Entries<IEntity>()
			.Where(
				e => e.Entity.GetDomainEvents()
					.Any())
			.ToList();

		var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents())
			.ToList();

		await _dbContext.SaveChangesAsync(cancellationToken);

		foreach (var domainEvent in domainEvents)
		{
			var domainEvenHandlerType = typeof(IDomainEventHandler<>);
			var domainEvenHandlerTypeWithGenericType = domainEvenHandlerType.MakeGenericType(domainEvent.GetType());

			foreach (dynamic? service in _serviceProvider.GetServices(domainEvenHandlerTypeWithGenericType))
			{
				var correlationId = Guid.NewGuid();

				_logger.LogInformation($"[{correlationId}] Executing domain event handler: '{service!.ToString()}' for event type: '{domainEvent.GetType().Name}'.");

				await service.HandleAsync((dynamic)domainEvent, cancellationToken);
				
				_logger.LogInformation($"[{correlationId}] Executed.");
			}
			
			domainEntities.Single(a => a.Entity
					.GetDomainEvents()
					.Contains(domainEvent))
				.Entity
				.RemoveDomainEvent(domainEvent);
		}

		return response;
	}
}
