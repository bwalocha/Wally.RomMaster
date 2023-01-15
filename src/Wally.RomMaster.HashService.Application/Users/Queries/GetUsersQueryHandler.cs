using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.Lib.DDD.Abstractions.Responses;
using Wally.RomMaster.HashService.Contracts.Requests.Users;
using Wally.RomMaster.HashService.Contracts.Responses.Users;

namespace Wally.RomMaster.HashService.Application.Users.Queries;

public class GetUsersQueryHandler : QueryHandler<GetUsersQuery, PagedResponse<GetUsersResponse>>
{
	private readonly IUserReadOnlyRepository _userRepository;

	public GetUsersQueryHandler(IUserReadOnlyRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public override Task<PagedResponse<GetUsersResponse>> HandleAsync(
		GetUsersQuery query,
		CancellationToken cancellationToken)
	{
		return _userRepository.GetAsync<GetUsersRequest, GetUsersResponse>(query.QueryOptions, cancellationToken);
	}
}
