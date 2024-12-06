using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence;
using MediatR;

namespace BloodBank.Application.Query.GetDoadoresAll
{
    public class GetDoadoresAllHandler : IRequestHandler<GetDoadoresAllQuery, ResultViewModel<List<Core.Entities.Doador>>>
    {
        private readonly BloodBankDbContext _db;
        private readonly IDoadorRepository _repository;
        public GetDoadoresAllHandler(BloodBankDbContext db, IDoadorRepository repository)
        {
            _db = db;
            _repository = repository;
        }

        public async Task<ResultViewModel<List<Core.Entities.Doador>>> Handle(GetDoadoresAllQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var (doadores, totalItems) = await _repository.GetAll(pageNumber, pageSize);

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

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
