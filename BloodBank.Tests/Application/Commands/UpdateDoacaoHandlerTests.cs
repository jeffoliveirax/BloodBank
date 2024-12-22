using BloodBank.Application.Commands.UpdateDoacao;
using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BloodBank.Tests.Application.Commands
{
    public class UpdateDoacaoHandlerTests
    {
        private readonly Mock<IDoacaoRepository> _repositoryMock;
        private readonly UpdateDoacaoHandler _handler;

        public UpdateDoacaoHandlerTests()
        {
            _repositoryMock = new Mock<IDoacaoRepository>();
            _handler = new UpdateDoacaoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DoacaoDoesNotExist_ReturnsErrorResult()
        {
            // Arrange
            var command = new UpdateDoacaoCommand(1, 1, 450);
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Doacao)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("A doação não existe.", result.Message);

            _repositoryMock.Verify(repo => repo.Update(It.IsAny<Doacao>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DoacaoExists_UpdatesDoacaoAndReturnsSuccessResult()
        {
            // Arrange
            var command = new UpdateDoacaoCommand(1, 1, 450);
            var doacao = new Doacao(1, 450);
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doacao);
            _repositoryMock.Setup(repo => repo.Update(It.IsAny<Doacao>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(repo => repo.Update(It.IsAny<Doacao>()), Times.Once);
        }
    }
}