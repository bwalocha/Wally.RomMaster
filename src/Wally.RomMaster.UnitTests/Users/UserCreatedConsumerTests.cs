using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Moq;

using Wally.IdentityProvider.Contracts.Messages;
using Wally.RomMaster.Application.Users.Commands;
using Wally.RomMaster.Messaging.Consumers;

using Xunit;

namespace Wally.RomMaster.UnitTests.Users;

public class UserCreatedConsumerTests
{
	private readonly UserCreatedConsumer _consumer;
	private readonly Mock<IMediator> _mediatorMock;

	public UserCreatedConsumerTests()
	{
		_mediatorMock = new Mock<IMediator>();
		_consumer = new UserCreatedConsumer(_mediatorMock.Object, new Mock<ILogger<UserCreatedConsumer>>().Object);
	}

	[Fact]
	public async Task ConsumeAsync_ForValidMessage_ShouldPublishCommand()
	{
		// Arrange
		var message = new UserCreatedMessage(Guid.NewGuid(), "testName", "test@email.com");

		// Act
		await _consumer.ConsumeAsync(message, CancellationToken.None);

		// Assert
		_mediatorMock.Verify(
			a => a.Send(
				It.Is<CreateUserCommand>(a => a.Id == message.UserId && a.Name == message.UserName),
				CancellationToken.None),
			Times.Once());
	}
}
