using BloodBank.Application.Commands.InsertDoador;
using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Enum;
using BloodBank.Core.Repositories;
using Moq;
using Xunit;

namespace BloodBank.Tests.Application.Commands
{
    public class InsertDoadorHandlerTests
    {
        private readonly Mock<IDoadorRepository> _repositoryMock;
        private readonly InsertDoadorHandler _handler;

        public InsertDoadorHandlerTests()
        {
            _repositoryMock = new Mock<IDoadorRepository>();
            _handler = new InsertDoadorHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task ValidRequest_HandleIsCalled_ReturnsSuccess()
        {
            // Arrange
            var command = new InsertDoadorCommand(
                "Fulano de tal",
                "fulano@email.com",
                new DateTime(2020, 11, 5),
                "Masculino",
                75.2,
                TipoSanguineo.A,
                FatorRh.Negativo,
                new Endereco
                {
                    Logradouro = "Rua 1",
                    Numero = 123,
                    Bairro = "Centro",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    CEP = "12345-678"
                }
            );

            var doador = new Doador(
                command.NomeCompleto,
                command.Email,
                command.DataNascimento,
                command.Genero,
                command.Peso,
                command.Endereco,
                command.TipoSanguineo,
                command.FatorRh
            );

            _repositoryMock.Setup(repo => repo.Exists(It.IsAny<string>())).ReturnsAsync(false);
            _repositoryMock.Setup(repo => repo.Insert(It.IsAny<Doador>())).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            _repositoryMock.Verify(repo => repo.Exists(It.IsAny<string>()), Times.Once);
            _repositoryMock.Verify(repo => repo.Insert(It.IsAny<Doador>()), Times.Once);
        }

        [Fact]
        public async Task EmailAlreadyExists_HandleIsCalled_ReturnsError()
        {
            // Arrange
            var command = new InsertDoadorCommand(
                "Fulano de tal",
                "fulano@email.com",
                new DateTime(2020, 11, 5),
                "Masculino",
                75.2,
                TipoSanguineo.A,
                FatorRh.Negativo,
                new Endereco
                {
                    Logradouro = "Rua 1",
                    Numero = 123,
                    Bairro = "Centro",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    CEP = "12345-678"
                }
            );

            _repositoryMock.Setup(repo => repo.Exists(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Já existe um usuário cadastrado com este e-mail", result.Message);

            _repositoryMock.Verify(repo => repo.Exists(It.IsAny<string>()), Times.Once);
        }
    }
}