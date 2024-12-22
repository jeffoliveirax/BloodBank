using BloodBank.Application.Commands.InsertDoacao;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Moq;

namespace BloodBank.Tests.Application.Commands
{
    public class InsertDoacaoHandlerTests
    {
        private readonly Mock<IDoacaoRepository> _doacaoRepositoryMock;
        private readonly Mock<IDoadorRepository> _doadorRepositoryMock;
        private readonly InsertDoacaoHandler _handler;

        public InsertDoacaoHandlerTests()
        {
            _doacaoRepositoryMock = new Mock<IDoacaoRepository>();
            _doadorRepositoryMock = new Mock<IDoadorRepository>();
            _handler = new InsertDoacaoHandler(_doacaoRepositoryMock.Object, _doadorRepositoryMock.Object);
        }

        [Fact]
        public async Task DoadorDoesNotExist_Executed_ReturnsErrorResult()
        {
            // Arrange
            var command = new InsertDoacaoCommand(1, DateTime.Today, 450);
            _doadorRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Doador)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Id de doador não existe!", result.Message);

            _doacaoRepositoryMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DoadorUnderage_Executed_ReturnsErrorResult()
        {
            // Arrange
            var command = new InsertDoacaoCommand(1, DateTime.Today, 450);
            var doador = new Doador { DataNascimento = DateTime.Today.AddYears(-17) };
            _doadorRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doador);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("O doador deve ter mais de 18 anos para efetuar uma doação.", result.Message);

            _doacaoRepositoryMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DoadorUnderweight_Executed_ReturnsErrorResult()
        {
            // Arrange
            var command = new InsertDoacaoCommand(1, DateTime.Today, 450);
            var doador = new Doador { DataNascimento = DateTime.Today.AddYears(-20), Peso = 49 };
            _doadorRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doador);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("O doador deve pesar no mínimo 50kg para efetuar uma doação.", result.Message);

            _doacaoRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Doacao>()), Times.Never);
        }

        [Fact]
        public async Task FemaleDoadorDonatedRecently_Executed_ReturnsErrorResult()
        {
            // Arrange
            var command = new InsertDoacaoCommand(1, DateTime.Today, 450);
            var doador = new Doador { DataNascimento = DateTime.Today.AddYears(-20), Peso = 55, Genero = "feminino" };
            var ultimaDoacao = new Doacao(doador.Id, 450);
            _doadorRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doador);
            _doacaoRepositoryMock.Setup(repo => repo.GetByDoador(It.IsAny<int>())).ReturnsAsync(ultimaDoacao);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Mulheres só podem doar de 90 em 90 dias.", result.Message);

            _doacaoRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Doacao>()), Times.Never);
        }

        [Fact]
        public async Task MaleDoadorDonatedRecently_Executed_ReturnsErrorResult()
        {
            // Arrange
            var command = new InsertDoacaoCommand(1, DateTime.Today, 450);
            var doador = new Doador { DataNascimento = DateTime.Today.AddYears(-20), Peso = 55, Genero = "masculino" };
            var ultimaDoacao = new Doacao(doador.Id, 450);
            _doadorRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doador);
            _doacaoRepositoryMock.Setup(repo => repo.GetByDoador(It.IsAny<int>())).ReturnsAsync(ultimaDoacao);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Homens só podem doar de 60 em 60 dias.", result.Message);

            _doacaoRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Doacao>()), Times.Never);
        }

        [Fact]
        public async Task InvalidVolume_Executed_ReturnsErrorResult()
        {
            // Arrange
            var command = new InsertDoacaoCommand(1, DateTime.Today, 400);
            var doador = new Doador { DataNascimento = DateTime.Today.AddYears(-20), Peso = 55, Genero = "masculino" };
            _doadorRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doador);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("A quantidade de mililitros de sangue doados deve ser entre 420ml e 470ml.", result.Message);

            _doacaoRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Doacao>()), Times.Never);
        }

        [Fact]
        public async Task ValidRequest_Executed_InsertsDoacaoAndReturnsSuccessResult()
        {
            // Arrange
            var command = new InsertDoacaoCommand(1, DateTime.Today, 450);
            var doador = new Doador { DataNascimento = DateTime.Today.AddYears(-20), Peso = 55, Genero = "masculino" };
            _doadorRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doador);
            _doacaoRepositoryMock.Setup(repo => repo.Insert(It.IsAny<Doacao>())).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            _doacaoRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Doacao>()), Times.Once);
        }
    }
}
