using BloodBank.Application.Query.GetDoadorById;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using BloodBank.Core.Enum;
using BloodBank.Application.Models;

namespace BloodBank.Tests.Application.Queries
{
    public class GetDoadorByIdHandlerTests
    {
        private readonly Mock<IDoadorRepository> _repositoryMock;
        private readonly GetDoadorByIdHandler _handler;

        public GetDoadorByIdHandlerTests()
        {
            _repositoryMock = new Mock<IDoadorRepository>();
            _handler = new GetDoadorByIdHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task DoadorExists_Executed_ReturnsSuccessResult()
        {
            // Arrange
            var doador = new DoadorBuilderTestModel()
                .ComId(1)
                .ComNome("John Doe")
                .Build();

            // Mock do repositório
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(doador);

            var query = new GetDoadorByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(doador.Id, result.Data.Id);
            Assert.Equal(doador.NomeCompleto, result.Data.NomeCompleto);

            _repositoryMock.Verify(repo => repo.GetById(query.Id), Times.Once);
        }

        [Fact]
        public async Task DoadorDoesNotExist_Executed_ReturnsErrorResult()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Doador)null);

            var query = new GetDoadorByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Não há doador cadastrado com esse Id.", result.Message);

            _repositoryMock.Verify(repo => repo.GetById(query.Id), Times.Once);
        }
    }
}