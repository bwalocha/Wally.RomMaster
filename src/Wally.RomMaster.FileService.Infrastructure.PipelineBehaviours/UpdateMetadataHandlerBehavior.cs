using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Infrastructure.PipelineBehaviours;

public class UpdateMetadataHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : ICommand, IRequest<TResponse>
{
	private readonly IClockService _clockService;
	private readonly DbContext _dbContext;
	private readonly IUserProvider _userProvider;

	public UpdateMetadataHandlerBehavior(
		DbContext dbContext,
		IUserProvider userProvider,
		IClockService dateTimeProvider)
	{
		_dbContext = dbContext;
		_userProvider = userProvider;
		_clockService = dateTimeProvider;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		var response = await next();

		UpdateAggregateMetadata(_dbContext.ChangeTracker.Entries<AggregateRoot>());

		return response;
	}

	private void UpdateAggregateMetadata(IEnumerable<EntityEntry<AggregateRoot>> entries)
	{
		var now = _clockService.GetTimestamp();

		foreach (var entry in entries)
		{
			switch (entry.State)
			{
				case EntityState.Modified:
					entry.CurrentValues.SetValues(
						new Dictionary<string, object>
						{
							{ nameof(AggregateRoot.ModifiedAt), now },
							{ nameof(AggregateRoot.ModifiedById), _userProvider.GetCurrentUserId() },
						});
					break;
				case EntityState.Added:
					entry.CurrentValues.SetValues(
						new Dictionary<string, object>
						{
							{ nameof(AggregateRoot.CreatedAt), now },
							{ nameof(AggregateRoot.CreatedById), _userProvider.GetCurrentUserId() },
						});
					break;
			}
		}
	}
}
