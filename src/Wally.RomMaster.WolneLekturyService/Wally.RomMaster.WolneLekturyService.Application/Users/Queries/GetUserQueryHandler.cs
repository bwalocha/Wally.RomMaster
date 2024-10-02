using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Responses;

namespace Wally.RomMaster.WolneLekturyService.Application.Users.Queries;

public class GetUserQueryHandler : QueryHandler<GetUserQuery, GetUserResponse>
{
	private readonly IUserReadOnlyRepository _userRepository;

	public GetUserQueryHandler(IUserReadOnlyRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public override Task<GetUserResponse> HandleAsync(GetUserQuery query, CancellationToken cancellationToken)
	{
		return _userRepository.GetAsync<GetUserResponse>(query.UserId, cancellationToken);
	}
}
