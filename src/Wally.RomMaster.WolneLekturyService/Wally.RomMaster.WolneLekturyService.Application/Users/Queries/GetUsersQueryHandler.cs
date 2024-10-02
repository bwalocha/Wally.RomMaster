using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Application.Contracts;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Requests;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Responses;

namespace Wally.RomMaster.WolneLekturyService.Application.Users.Queries;

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
