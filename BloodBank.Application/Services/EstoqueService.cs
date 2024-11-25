using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;

namespace BloodBank.Application.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly BloodBankDbContext _db;
        public EstoqueService(BloodBankDbContext db)
            => _db = db;

        public ResultViewModel<List<Estoque>> GetAll()
        {
            List<Estoque> estoque = CalcularEstoque(_db.Doacoes.ToList(), _db.Doadores.ToList());

            if (estoque.Any())
            {
                return ResultViewModel<List<Estoque>>.Success(estoque);
            }
            else
            {
                return ResultViewModel<List<Estoque>>.Error("Não há estoque disponível!");
            }
        }

        public ResultViewModel<List<Estoque>> GetByDate()
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-30);

            var doacoes = _db.Doacoes
                            .Where(d => d.Data >= startDate && d.Data <= endDate)
                            .ToList();

            if (doacoes.Any())
            {
                List<Estoque> estoque = CalcularEstoque(doacoes, _db.Doadores.ToList());

                return ResultViewModel<List<Estoque>>.Success(estoque);
            }
            else
            {
                return ResultViewModel<List<Estoque>>.Error("Não há doações no período!");
            }
        }

        private static List<Estoque> CalcularEstoque(List<Doacao> doacoes, List<Doador> doadores)
        {
            var estoque = doacoes
                .Join(doadores,
                      doacao => doacao.DoadorId,
                      doador => doador.Id,
                      (doacao, doador) => new { doacao, doador })
                .GroupBy(d => new { d.doador.TipoSanguineo, d.doador.FatorRh })
                .Select(grupo => new Estoque
                {
                    TipoSanguineo = grupo.Key.TipoSanguineo,
                    FatorRh = grupo.Key.FatorRh,
                    Volume = grupo.Sum(d => d.doacao.Volume)
                })
                .ToList();

            return estoque;
        }
    }
}
