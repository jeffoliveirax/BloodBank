using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;
using MediatR;

namespace BloodBank.Application.Query.GetDoadoresAll
{
    public class GetDoadoresAllHandler : IRequestHandler<GetDoadoresAllQuery, ResultViewModel<List<Doador>>>
    {
        private readonly BloodBankDbContext _db;
        public GetDoadoresAllHandler(BloodBankDbContext db) => _db = db;
        public async Task<ResultViewModel<List<Doador>>> Handle(GetDoadoresAllQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var totalItems = _db.Doadores.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var doadores = _db.Doadores
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();
            if (doadores is null)
                return ResultViewModel<List<Doador>>.Error("Não há doadores cadastrados.");

            var response = new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = doadores
            };

            return ResultViewModel<List<Doador>>.Success(response.Data);
        }
    }
}
