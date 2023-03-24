﻿using Microsoft.Extensions.DependencyInjection;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.RomMaster.HashService.Application;
using Wally.RomMaster.HashService.Infrastructure.PipelineBehaviours;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Extensions;

public static class CqrsExtensions
{
	public static IServiceCollection AddCqrs(this IServiceCollection services)
	{
		services.AddMediatR(
			a =>
			{
				a.RegisterServicesFromAssemblyContaining<IApplicationAssemblyMarker>();

				a.AddOpenBehavior(typeof(LogBehavior<,>));
				a.AddOpenBehavior(typeof(TransactionBehavior<,>));
				a.AddOpenBehavior(typeof(UpdateMetadataHandlerBehavior<,>));
				a.AddOpenBehavior(typeof(DomainEventHandlerBehavior<,>));
				a.AddOpenBehavior(typeof(UpdateMetadataHandlerBehavior<,>));
				a.AddOpenBehavior(typeof(CommandHandlerValidatorBehavior<,>));
				a.AddOpenBehavior(typeof(QueryHandlerValidatorBehavior<,>));
			});

		services.Scan(
			a => a.FromAssemblyOf<IApplicationAssemblyMarker>()
				.AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
				.AsImplementedInterfaces()
				.WithScopedLifetime());

		return services;
	}
}