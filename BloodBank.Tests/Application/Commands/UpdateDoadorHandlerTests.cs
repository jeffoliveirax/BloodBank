using BloodBank.Application.Commands.UpdateDoador;
using BloodBank.Core.Entities;
using BloodBank.Core.Enum;
using BloodBank.Core.Repositories;
using Moq;

namespace BloodBank.Tests.Application.Commands
{
    public class UpdateDoadorHandlerTests
    {
        private readonly Mock<IDoadorRepository> _repositoryMock;
        private readonly UpdateDoadorHandler _handler;

        public UpdateDoadorHandlerTests()
        {
            _repositoryMock = new Mock<IDoadorRepository>();
            _handler = new UpdateDoadorHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task DoadorDoesNotExist_Executed_ReturnsErrorResult()
        {
            // Arrange
            var command = new UpdateDoadorCommand(1, "email@example.com", DateTime.Today, "masculino", 70, new Endereco(), TipoSanguineo.O, FatorRh.Positivo);
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Doador)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Este doador não existe.", result.Message);

            _repositoryMock.Verify(repo => repo.Update(It.IsAny<Doador>()), Times.Never);
        }

        [Fact]
        public async Task DoadorExists_Executed_UpdatesDoadorAndReturnsSuccessResult()
        {
            // Arrange
            var command = new UpdateDoadorCommand(1, "email@example.com", DateTime.Today, "masculino", 70, new Endereco(), TipoSanguineo.O, FatorRh.Positivo);
            var doador = new Doador();
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doador);
            _repositoryMock.Setup(repo => repo.Update(It.IsAny<Doador>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(repo => repo.Update(It.IsAny<Doador>()), Times.Once);
        }
    }
}