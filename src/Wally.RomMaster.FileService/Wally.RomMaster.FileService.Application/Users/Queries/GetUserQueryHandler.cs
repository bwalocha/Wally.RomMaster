using System.Threading;
using System.Threading.Tasks;
using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Users;

namespace Wally.RomMaster.FileService.Application.Users.Queries;

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
