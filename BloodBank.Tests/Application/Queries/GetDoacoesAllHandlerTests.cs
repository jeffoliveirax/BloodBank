using BloodBank.Application.Models;
using BloodBank.Application.Query.GetDoacoesAll;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Moq;

namespace BloodBank.Tests.Application.Queries
{
    public class GetDoacoesAllHandlerTests
    {
        private readonly Mock<IDoacaoRepository> _repositoryMock;
        private readonly GetDoacoesAllHandler _handler;

        public GetDoacoesAllHandlerTests()
        {
            _repositoryMock = new Mock<IDoacaoRepository>();
            _handler = new GetDoacoesAllHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task DoacoesExist_Executed_ReturnsSuccessResult()
        {
            // Arrange
            var doacoes = new List<Doacao>
            {
                new Doacao(1, 450)
                {
                    Data = DateTime.Today,
                    Doador = new Doador
                    {
                        NomeCompleto = "John Doe"
                    }
                },
                new Doacao(2, 445)
                {
                    Data = DateTime.Today,
                    Doador = new Doador
                    {
                        NomeCompleto = "Jane Doe"
                    }
                }
            };

            _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(doacoes);

            var query = new GetDoacoesAllQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(doacoes.Count, result.Data.Count);
            Assert.Equal(doacoes[0].Id, result.Data[0].Id);
            Assert.Equal(doacoes[0].Doador.NomeCompleto, result.Data[0].NomeDoador);

            _repositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_NoDoacoesExist_ReturnsErrorResult()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync((List<Doacao>)null);

            var query = new GetDoacoesAllQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Não existem doações.", result.Message);

            _repositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}