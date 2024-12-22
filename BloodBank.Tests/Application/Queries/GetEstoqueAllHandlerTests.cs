using BloodBank.Application.Query.GetEstoque;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Moq;
using BloodBank.Core.Enum;
using System.Reflection;

namespace BloodBank.Tests.Application.Queries
{
    public class GetEstoqueAllHandlerTests
    {
        private readonly Mock<IDoacaoRepository> _doacaoRepositoryMock;
        private readonly Mock<IDoadorRepository> _doadorRepositoryMock;
        private readonly GetEstoqueAllHandler _handler;

        public GetEstoqueAllHandlerTests()
        {
            _doacaoRepositoryMock = new Mock<IDoacaoRepository>();
            _doadorRepositoryMock = new Mock<IDoadorRepository>();
            _handler = new GetEstoqueAllHandler(_doacaoRepositoryMock.Object, _doadorRepositoryMock.Object);
        }

        //[Fact]
        //public async Task EstoqueExists_Executed_ReturnsSuccessResult()
        //{
        //    // Arrange
        //    var doacoes = new List<Doacao>
        //    {
        //        new Doacao(1, 450)
        //        {
        //            Data = DateTime.Today,
        //            Doador = new Doador
        //            {
        //                NomeCompleto = "John Doe",
        //                TipoSanguineo = TipoSanguineo.O,
        //                FatorRh = FatorRh.Positivo
        //            }
        //        },
        //        new Doacao(2, 500)
        //        {
        //            Data = DateTime.Today,
        //            Doador = new Doador
        //            {
        //                NomeCompleto = "Jane Doe",
        //                TipoSanguineo = TipoSanguineo.A,
        //                FatorRh = FatorRh.Negativo
        //            }
        //        }
        //    };

        //    doacoes[0].GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic)
        //        ?.SetValue(doacoes[0], 1);
        //    doacoes[1].GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic)
        //        ?.SetValue(doacoes[1], 2);

        //    var doadores = new List<Doador>
        //    {
        //        new Doador
        //        {
        //            NomeCompleto = "John Doe",
        //            TipoSanguineo = TipoSanguineo.O,
        //            FatorRh = FatorRh.Positivo
        //        },
        //        new Doador
        //        {
        //            NomeCompleto = "Jane Doe",
        //            TipoSanguineo = TipoSanguineo.A,
        //            FatorRh = FatorRh.Negativo
        //        }
        //    };

        //    doadores[0].GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic)
        //        ?.SetValue(doadores[0], 1);
        //    doadores[1].GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic)
        //        ?.SetValue(doadores[1], 2);

        //    _doacaoRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(doacoes);
        //    _doadorRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(doadores);

        //    var query = new GetEstoqueAllQuery();

        //    // Act
        //    var result = await _handler.Handle(query, CancellationToken.None);

        //    // Assert
        //    Assert.NotNull(result);
        //    Console.WriteLine($"Result: IsSuccess = {result.IsSuccess}, Message = {result.Message}");
        //    Assert.True(result.IsSuccess, result.Message);
        //    Assert.NotNull(result.Data);
        //    Assert.Equal(2, result.Data.Count);

        //    _doacaoRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        //    _doadorRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        //}

        [Fact]
        public async Task NoEstoqueExists_Executed_ReturnsErrorResult()
        {
            // Arrange
            _doacaoRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Doacao>());
            _doadorRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Doador>());

            var query = new GetEstoqueAllQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Não há estoque disponível!", result.Message);

            _doacaoRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _doadorRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}