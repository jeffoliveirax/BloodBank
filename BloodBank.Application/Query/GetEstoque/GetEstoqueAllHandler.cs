using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;
using MediatR;

namespace BloodBank.Application.Query.GetEstoque
{
    public class GetEstoqueAllHandler : IRequestHandler<GetEstoqueAllQuery, ResultViewModel<List<Estoque>>>
    {
        private readonly BloodBankDbContext _db;
        public GetEstoqueAllHandler(BloodBankDbContext db)
            => _db = db;
        public async Task<ResultViewModel<List<Estoque>>> Handle(GetEstoqueAllQuery request, CancellationToken cancellationToken)
        {
            List<Estoque> estoque = Estoque.CalcularEstoque(_db.Doacoes.ToList(), _db.Doadores.ToList());

            if (estoque.Count != 0)
            {
                return ResultViewModel<List<Estoque>>.Success(estoque);
            }
            else
            {
                return ResultViewModel<List<Estoque>>.Error("Não há estoque disponível!");
            }
        }
    }
}
