using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.PipelineBehaviours;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : ICommand, IRequest<TResponse>
{
	private readonly DbContext _dbContext;
	private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

	public TransactionBehavior(DbContext dbContext, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
	{
		_dbContext = dbContext;
		_logger = logger;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		CancellationToken cancellationToken,
		RequestHandlerDelegate<TResponse> next)
	{
		await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			var response = await next();

			// UpdateAggregateMetadata(_dbContext.ChangeTracker.Entries<AggregateRoot>());

			var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

			await transaction.CommitAsync(cancellationToken);

			_logger.LogDebug($"Rows affected: '{rowsAffected}'");
			
			return response;
		}
		catch
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
	}

	/*private void UpdateAggregateMetadata(IEnumerable<EntityEntry<AggregateRoot>> entries)
	{
		foreach (var entry in entries)
		{
			switch (entry.State)
			{
				case EntityState.Modified:
					entry.CurrentValues.SetValues(
						new Dictionary<string, object>
						{
							{
								nameof(AggregateRoot.ModifiedAt), DateTime.Now
							},
							{
								nameof(AggregateRoot.ModifiedById), _userProvider.GetUser()
									.Id
							}
						});
					break;
				case EntityState.Added:
					entry.CurrentValues.SetValues(
						new Dictionary<string, object>
						{
							{
								nameof(AggregateRoot.CreatedAt), DateTime.Now
							},
							{
								nameof(AggregateRoot.CreatedById), _userProvider.GetUser()
									.Id
							},
						});
					break;
			}
		}
	}*/
}
