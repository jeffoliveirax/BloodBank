using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Query.GetDoadoresAll
{
    public class GetDoadoresAllHandler : IRequestHandler<GetDoadoresAllQuery, ResultViewModel<List<Core.Entities.Doador>>>
    {
        private readonly IDoadorRepository _repository;
        public GetDoadoresAllHandler(IDoadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<Doador>>> Handle(GetDoadoresAllQuery request, CancellationToken cancellationToken)
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
            //return ResultViewModel<List<Doador>>.Success(new List<Doador>()); //fazer o teste falhar
        }
    }
}
