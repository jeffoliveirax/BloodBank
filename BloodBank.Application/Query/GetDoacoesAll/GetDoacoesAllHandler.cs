using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Query.GetDoacoesAll
{
    public class GetDoacoesAllHandler(BloodBankDbContext db) : IRequestHandler<GetDoacoesAllQuery, ResultViewModel<List<DoacoesItemViewModel>>>
    {
        private readonly BloodBankDbContext _db = db;

        public async Task<ResultViewModel<List<DoacoesItemViewModel>>> Handle(GetDoacoesAllQuery request, CancellationToken cancellationToken)
        {
            var doacoes = await _db.Doacoes
               .Include(d => d.Doador)
               .ToListAsync();

            if (doacoes is null)
                ResultViewModel<List<DoacoesItemViewModel>>.Error("Não existem doações.");

            var model = doacoes.Select(DoacoesItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<DoacoesItemViewModel>>.Success(model);
        }
    }
}
