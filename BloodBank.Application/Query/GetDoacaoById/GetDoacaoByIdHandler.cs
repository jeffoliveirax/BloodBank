using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Query.GetDoacaoById
{
    public class GetDoacaoByIdHandler : IRequestHandler<GetDoacaoByIdQuery, ResultViewModel<DoacaoViewModel>>
    {
        private readonly BloodBankDbContext _db;
        public GetDoacaoByIdHandler(BloodBankDbContext db)
        {
            _db = db;
        }
        public async Task<ResultViewModel<DoacaoViewModel>> Handle(GetDoacaoByIdQuery request, CancellationToken cancellationToken)
        {
            var doacoes = await _db.Doacoes
               .Include(d => d.Doador)
               .SingleOrDefaultAsync(d => d.Id == request.Id);

            if (doacoes is null)
                return ResultViewModel<DoacaoViewModel>.Error("Doação não existe.");

            var model = DoacaoViewModel.FromEntity(doacoes);

            return ResultViewModel<DoacaoViewModel>.Success(model);
        }
    }
}
