﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Domain.Abstractions;

namespace Wally.RomMaster.NotificationService.Infrastructure.PipelineBehaviours;

public class DomainEventHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : ICommand<TResponse>
{
	private readonly DbContext _dbContext;
	private readonly IServiceProvider _serviceProvider;

	public DomainEventHandlerBehavior(DbContext dbContext, IServiceProvider serviceProvider)
	{
		_dbContext = dbContext;
		_serviceProvider = serviceProvider;
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
				await service!.HandleAsync((dynamic)domainEvent, cancellationToken);
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
