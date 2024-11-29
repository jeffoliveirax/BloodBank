using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Query.GetDoadorById
{
    public class GetDoadorByIdHandler : IRequestHandler<GetDoadorByIdQuery, ResultViewModel<Doador>>
    {
        private readonly BloodBankDbContext _db;
        public GetDoadorByIdHandler(BloodBankDbContext db)
        {
            _db = db;
        }

        public async Task<ResultViewModel<Doador>> Handle(GetDoadorByIdQuery request, CancellationToken cancellationToken)
        {
            var doador = await _db.Doadores.Where(d => d.Id == request.Id).FirstOrDefaultAsync();

            if (doador is null)
                return ResultViewModel<Doador>.Error("Não há doador cadastrado com esse Id.");

            return ResultViewModel<Doador>.Success(doador);
        }
    }
}
