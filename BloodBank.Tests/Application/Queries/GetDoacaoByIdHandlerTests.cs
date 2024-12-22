using BloodBank.Application.Models;
using BloodBank.Application.Query.GetDoacaoById;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Moq;
using System.Reflection;

namespace BloodBank.Tests.Application.Query
{
    public class GetDoacaoByIdHandlerTests
    {
        private readonly Mock<IDoacaoRepository> _repositoryMock;
        private readonly GetDoacaoByIdHandler _handler;

        public GetDoacaoByIdHandlerTests()
        {
            _repositoryMock = new Mock<IDoacaoRepository>();
            _handler = new GetDoacaoByIdHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task DoacaoDoesNotExist_Executed_ReturnsErrorResult()
        {
            // Arrange
            var query = new GetDoacaoByIdQuery(1);
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Doacao)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Doação não existe.", result.Message);

            _repositoryMock.Verify(repo => repo.GetById(query.Id), Times.Once);
        }

        [Fact]
        public async Task DoacaoExists_Executed_ReturnsSuccessResultWithDoacaoViewModel()
        {
            // Arrange
            var doador = new DoadorBuilderTestModel()
                .ComId(1)
                .ComNome("John Doe")
                .Build();

            var doacao = new DoacaoBuilderTestModel()
                .ComId(1)
                .ComDoador(doador)
                .ComVolume(450)
                .ComData(DateTime.Today)
                .Build();

            var query = new GetDoacaoByIdQuery(1);

            _repositoryMock.Setup(repo => repo.GetById(1))
                .ReturnsAsync(doacao);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(doacao.Id, result.Data.Id);
            Assert.Equal(doacao.DoadorId, result.Data.DoadorId);
            Assert.Equal(doacao.Data, result.Data.Data);
            Assert.Equal(doacao.Volume, result.Data.Volume);
            Assert.Equal(doador.NomeCompleto, result.Data.NomeDoador);
        }
    }
}