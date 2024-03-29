﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wally.RomMaster.HashService.Application;
using Wally.RomMaster.HashService.Infrastructure.PipelineBehaviours;
using Wally.Lib.DDD.Abstractions.DomainEvents;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Extensions;

public static class CqrsExtensions
{
	public static IServiceCollection AddCqrs(this IServiceCollection services)
	{
		services.AddMediatR(
			a =>
			{
				a.RegisterServicesFromAssemblyContaining<IApplicationAssemblyMarker>();
				/*
				a.AddOpenBehavior(typeof(LogBehavior<,>));
				a.AddOpenBehavior(typeof(TransactionBehavior<,>));
				a.AddOpenBehavior(typeof(UpdateMetadataHandlerBehavior<,>));
				a.AddOpenBehavior(typeof(DomainEventHandlerBehavior<,>));
				a.AddOpenBehavior(typeof(UpdateMetadataHandlerBehavior<,>));

				// a.AddOpenBehavior(typeof(CommandHandlerValidatorBehavior<,>));
				a.AddOpenBehavior(typeof(CommandHandlerValidatorsBehavior<,>));
				a.AddOpenBehavior(typeof(QueryHandlerValidatorBehavior<,>));
				*/
			});

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
		// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
		// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UpdateMetadataHandlerBehavior<,>));
		// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainEventHandlerBehavior<,>));
		// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UpdateMetadataHandlerBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandHandlerValidatorBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryHandlerValidatorBehavior<,>));

		services.Scan(
			a => a.FromAssemblyOf<IApplicationAssemblyMarker>()
				.AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
				.AsImplementedInterfaces()
				.WithScopedLifetime());

		return services;
	}
}
