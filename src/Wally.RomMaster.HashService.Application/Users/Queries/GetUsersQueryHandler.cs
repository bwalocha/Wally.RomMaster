using System.Threading;
using System.Threading.Tasks;

using Wally.RomMaster.HashService.Application.Contracts.Requests.Users;
using Wally.RomMaster.HashService.Application.Contracts.Responses.Users;
using Wally.Lib.DDD.Abstractions.Queries;
using Wally.Lib.DDD.Abstractions.Responses;

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
