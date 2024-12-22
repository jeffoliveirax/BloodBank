using BloodBank.Application.Commands.DeleteDoador;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Moq;

namespace BloodBank.Tests.Application.Commands
{
    public class DeleteDoadorHandlerTests
    {
        private readonly Mock<IDoadorRepository> _repositoryMock;
        private readonly DeleteDoadorHandler _handler;

        public DeleteDoadorHandlerTests()
        {
            _repositoryMock = new Mock<IDoadorRepository>();
            _handler = new DeleteDoadorHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task DoadorDoesNotExist_Executed_ReturnsErrorResult()
        {
            // Arrange
            var command = new DeleteDoadorCommand(1);
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Doador)null);
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Este doador não existe.", result.Message);

            _repositoryMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DoadorExists_Executed_DeletesDoadorAndReturnsSuccessResult()
        {
            // Arrange
            var command = new DeleteDoadorCommand(1);
            var doador = new Doador();
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doador);
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}