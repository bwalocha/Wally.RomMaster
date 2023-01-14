using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Wally.RomMaster.HashService.Application.Users;
using Wally.RomMaster.HashService.Application.Users.Queries;
using Wally.RomMaster.HashService.PipelineBehaviours;
using Wally.Lib.DDD.Abstractions.DomainEvents;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Extensions;

public static class CqrsExtensions
{
	public static IServiceCollection AddCqrs(this IServiceCollection services)
	{
		services.AddMediatR(typeof(GetUserQuery));

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainEventHandlerBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandHandlerValidatorBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryHandlerValidatorBehavior<,>));

		services.Scan(
			a => a.FromAssemblyOf<UserCreatedDomainEventHandler>()
				.AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
				.AsImplementedInterfaces()
				.WithScopedLifetime());

		return services;
	}
}
