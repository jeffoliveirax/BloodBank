using BloodBank.Application.Commands.DeleteDoacao;
using BloodBank.Core.Repositories;
using Moq;

namespace BloodBank.Tests.Application.Commands
{
public class DeleteDoacaoHandlerTests
    {
        private readonly Mock<IDoacaoRepository> _repositoryMock;
        private readonly DeleteDoacaoHandler _handler;

        public DeleteDoacaoHandlerTests()
        {
            _repositoryMock = new Mock<IDoacaoRepository>();
            _handler = new DeleteDoacaoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task DoacaoDoesNotExist_HandleIsCalled_ReturnsErrorResult()
        {
            // Arrange
            var command = new DeleteDoacaoCommand(1);
            _repositoryMock.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(false);
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("A doação não existe.", result.Message);

            _repositoryMock.Verify(repo => repo.Exists(It.IsAny<int>()), Times.Once);
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DoacaoExists_HandleIsCalled_DeletesDoacaoAndReturnsSuccessResult()
        {
            // Arrange
            var command = new DeleteDoacaoCommand(1);
            _repositoryMock.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(true);
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(repo => repo.Exists(It.IsAny<int>()), Times.Once);
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
