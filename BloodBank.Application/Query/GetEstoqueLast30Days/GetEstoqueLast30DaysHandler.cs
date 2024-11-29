using BloodBank.Application.Models;
using BloodBank.Application.Query.GetEstoque;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;
using MediatR;

namespace BloodBank.Application.Query.GetEstoqueLast30Days
{
    public class GetEstoqueLast30DaysHandler : IRequestHandler<GetEstoqueLast30DaysQuery, ResultViewModel<List<Estoque>>>
    {
        private readonly BloodBankDbContext _db;
        public GetEstoqueLast30DaysHandler(BloodBankDbContext db)
            => _db = db;
        public async Task<ResultViewModel<List<Estoque>>> Handle(GetEstoqueLast30DaysQuery request, CancellationToken cancellationToken)
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-30);

            var doacoes = _db.Doacoes
                            .Where(d => d.Data >= startDate && d.Data <= endDate)
                            .ToList();

            if (doacoes.Count != 0)
            {
                List<Estoque> estoque = Estoque.CalcularEstoque(doacoes, _db.Doadores.ToList());

                return ResultViewModel<List<Estoque>>.Success(estoque);
            }
            else
            {
                return ResultViewModel<List<Estoque>>.Error("Não há doações no período!");
            }
        }
    }
}
