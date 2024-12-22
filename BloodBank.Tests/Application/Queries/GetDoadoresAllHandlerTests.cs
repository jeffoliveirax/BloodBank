using BloodBank.Core.Entities;
using BloodBank.Core.Enum;
using BloodBank.Core.Repositories;
using BloodBank.Application.Query.GetDoadoresAll;
using Moq;

namespace BloodBank.Tests.Application.Queries
{
    public class GetDoadoresAllHandlerTests
    {
        private readonly Mock<IDoadorRepository> _repositoryMock;
        private readonly GetDoadoresAllHandler _handler;

        public GetDoadoresAllHandlerTests()
        {
            _repositoryMock = new Mock<IDoadorRepository>();
            _handler = new GetDoadoresAllHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task ValidRequest_HandleIsCalled_ReturnsDoadores()
        {
            // Arrange
            var doadores = new List<Doador>
            {
                new("Fulano de tal", "fulano@email.com", new DateTime(2020, 11, 5), "Masculino", 75.2, new Endereco
                {
                    Logradouro = "Rua 1",
                    Numero = 123,
                    Bairro = "Centro",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    CEP = "12345-678"
                }, TipoSanguineo.A, FatorRh.Negativo),
                new("Ciclano de tal", "ciclano@email.com", new DateTime(1999, 8, 22), "Feminino", 65.5, new Endereco
                {
                    Logradouro = "Rua 2",
                    Numero = 456,
                    Bairro = "Centro",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    CEP = "12345-678"
                }, TipoSanguineo.B, FatorRh.Positivo)
            };

            var totalItems = 2;
            _repositoryMock.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((doadores, totalItems));

            var query = new GetDoadoresAllQuery(1, 10);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(doadores, result.Data);

            _repositoryMock.Verify(repo => repo.GetAll(1, 10), Times.Once);
        }

        [Fact]
        public async Task NoDoadores_HandleIsCalled_ReturnsError()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((null, 0));

            var query = new GetDoadoresAllQuery(1, 10);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Não há doadores cadastrados.", result.Message);
        }
    }

}

